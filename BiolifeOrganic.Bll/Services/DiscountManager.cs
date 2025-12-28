
using AutoMapper;
using BiolifeOrganic.Bll.Services.Contracts;
using BiolifeOrganic.Bll.Services;
using BiolifeOrganic.Bll.ViewModels.Discount;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.Reprositories.Contracts;



public class DiscountManager : CrudManager<Discount, DiscountViewModel, CreateDiscountViewModel, UpdateDiscountViewModel>, IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IUserDiscountRepository _userDiscountRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly AppDbContext _context;

    public DiscountManager(
        IDiscountRepository discountRepository,
        IUserDiscountRepository userDiscountRepository,
        IOrderRepository orderRepository,
        AppDbContext context,
        IMapper mapper)
        : base(discountRepository, mapper)
    {
        _discountRepository = discountRepository;
        _userDiscountRepository = userDiscountRepository;
        _orderRepository = orderRepository;
        _context = context;
    }

    private DiscountValidationResult Invalid(string error)
        => new() { IsValid = false, Error = error };

    public async Task<DiscountValidationResult> ValidateAsync(string code, string? userId, decimal totalAmount)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Invalid("No discount code provided");

        var discount = await _discountRepository
            .GetValidByCodeAsync(code, DateTime.UtcNow);

        if (discount == null)
            return Invalid("Invalid discount code");

        if (discount.MaxUsageCount.HasValue &&
            discount.UsedCount >= discount.MaxUsageCount)
            return Invalid("Discount limit reached");

        if (!string.IsNullOrEmpty(userId))
        {
            if (discount.OnlyForNewUsers)
            {
                if (await _orderRepository.HasOrdersAsync(userId))
                    return Invalid("Discount only for new users");

                if (!await _userDiscountRepository.ExistsAsync(userId, discount.Id))
                {
                    await _userDiscountRepository.AddAsync(new UserDiscount
                    {
                        AppUserId = userId,
                        DiscountId = discount.Id,
                        IsUsed = false
                    });

                    await _context.SaveChangesAsync();
                }
            }

            if (discount.OnlyForExistingUsers &&
                !await _orderRepository.HasOrdersAsync(userId))
            {
                return Invalid("Discount only for existing users");
            }
        }

        return new DiscountValidationResult
        {
            IsValid = true,
            Percentage = discount.Percentage,
            DiscountId = discount.Id
        };
    }

    public async Task MarkAsUsedAsync(int discountId, string userId)
    {
        await _userDiscountRepository.MarkAsUsedAsync(userId, discountId);
        await _discountRepository.IncrementUsedCountAsync(discountId);
        await _context.SaveChangesAsync();
    }

    public async Task AssignWelcomeDiscountAsync(string userId)
    {
        var discount = await _discountRepository
            .GetValidByCodeAsync("WELCOME15", DateTime.UtcNow);

        if (discount == null)
            return;

        if (await _userDiscountRepository.ExistsAsync(userId, discount.Id))
            return;

        await _userDiscountRepository.AddAsync(new UserDiscount
        {
            AppUserId = userId,
            DiscountId = discount.Id,
            IsUsed = false
        });

        await _context.SaveChangesAsync();
    }
}

