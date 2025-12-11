document.addEventListener('DOMContentLoaded', function () {
    const searchBox = document.getElementById("searchBox");
    const categorySelect = document.getElementById("categorySelect");
    const resultsDiv = document.getElementById("liveSearchResults");
    const form = document.querySelector('.form-search');

    async function fetchResults() {
        let text = searchBox.value.trim();
        let category = categorySelect.value;

        if (text === "" && category === "") {
            resultsDiv.innerHTML = `<div class="result-item">Enter text or select a category to search.</div>`;
            resultsDiv.style.display = "block";
            return;
        }

        const response = await fetch(`/Search/LivePartial?searchText=${encodeURIComponent(text)}&categoryId=${category}`);
        const html = await response.text();
        resultsDiv.innerHTML = html;
        resultsDiv.style.display = "block";
    }

    searchBox.addEventListener("keyup", fetchResults);
    categorySelect.addEventListener("change", fetchResults);

    form.addEventListener('submit', function (e) {
        const searchText = searchBox.value.trim();
        const categoryId = categorySelect.value;

        if (searchText === "" && categoryId === "") {
            e.preventDefault();
            resultsDiv.innerHTML = `<div class="result-item">Enter text or select a category to search.</div>`;
            resultsDiv.style.display = "block";
        }
    });
});
