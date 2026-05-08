
let allProducts = [];

// Run after page loads
$(document).ready(function () {
    loadProducts();

    // Search
    $("#searchBox").on("input", function () {
        let keyword = $(this).val().toLowerCase();

        let filtered = allProducts.filter(p =>
            p.name.toLowerCase().includes(keyword)
        );

        displayProducts(filtered);
    });
});

// Load JSON
function loadProducts() {
    $.getJSON("data/products.json", function (data) {

        console.log("Products loaded:", data); // debug

        allProducts = data;
        displayProducts(data);

    }).fail(function () {
        alert("Error loading products.json  Check file path");
    });
}

// Display products
function displayProducts(products) {
    let html = "";

    if (products.length === 0) {
        html = `<h4 class="text-center mt-4">No products found </h4>`;
    } else {

        products.forEach(p => {
            html += `
            <div class="col-md-3 mb-4">
                <div class="card product-card h-100">

                    <img src="${p.image}" class="product-img"
                         onerror="this.src='https://via.placeholder.com/200?text=No+Image'">

                    <div class="card-body text-center d-flex flex-column">

                        <h6 class="card-title">${p.name}</h6>
                        <p class="price">₹${p.price.toLocaleString()}</p>

                        <div class="mt-auto">
                            <a href="product-details.html?id=${p.id}" 
                               class="btn btn-info btn-sm w-100 mb-2">View</a>

                            <button class="btn btn-primary btn-sm w-100" 
                                    onclick="addToCartById(${p.id})">
                                Add to Cart
                            </button>
                        </div>

                    </div>
                </div>
            </div>`;
        });
    }

    $("#productList").html(html);
}

// Safe function
function addToCartById(id) {
    let product = allProducts.find(p => p.id === id);

    if (!product) {
        alert("Product not found ");
        return;
    }

    addToCart(product);
}