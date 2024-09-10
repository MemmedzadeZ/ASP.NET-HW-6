// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//// Write your JavaScript code.



const searchInput = document.getElementById("searchInput");

searchInput.addEventListener('keyup', function () {
    const query = searchInput.value;

    if (!query.length > 0) fetchtProduct("");
    else fetchtProduct(query);
})
function displayResult(product) {
    try {
        const currentCategory = '@Model.CurrentCategory';
        const currentPage = '@Model.CurrentPage';

        fetch('/Product/SerachProducts?searchTerm=${serchTerm}&{currentCategory}&page={currentPage},
        {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

        if (!response.ok) throw new Error("Error fetching product text");
        else {
            const data = await response.text();
            document.getElementById('productList').innerHTML = data;

        }
    }
    catch (error) {
        Error();
    }
}