

// Run when page loads
$(document).ready(function () {
    loadCart();          // display cart items
    updateCartCount();   // update cart count
});

// Add product to cart
function addToCart(product) {
    let cart = JSON.parse(localStorage.getItem("cart")) || [];

    // Check if already exists
    let exist = cart.find(p => p.id === product.id);

    if (exist) {
        exist.qty += 1; // increase qty
    } else {
        product.qty = 1;
        cart.push(product);
    }

    // Save to localStorage
    localStorage.setItem("cart", JSON.stringify(cart));

    updateCartCount();
    alert("Added to cart");
}

// Load cart items
function loadCart() {
    let cart = JSON.parse(localStorage.getItem("cart")) || [];

    let html = "";
    let total = 0;

    cart.forEach((item, i) => {
        total += item.price * item.qty;

        html += `
        <tr>
            <td>${item.name}</td>
            <td>${item.price}</td>

            <!-- Quantity controls -->
            <td>
                <button onclick="changeQty(${i},-1)">-</button>
                ${item.qty}
                <button onclick="changeQty(${i},1)">+</button>
            </td>

            <!-- Remove button -->
            <td>
                <button onclick="removeItem(${i})">Remove</button>
            </td>
        </tr>`;
    });

    $("#cartTable").html(html);
    $("#total").text(total);
}

// Change quantity
function changeQty(i, val) {
    let cart = JSON.parse(localStorage.getItem("cart"));

    cart[i].qty += val;

    // Remove if qty = 0
    if (cart[i].qty <= 0) {
        cart.splice(i, 1);
    }

    localStorage.setItem("cart", JSON.stringify(cart));

    loadCart();
    updateCartCount();
}

// Remove item
function removeItem(i) {
    let cart = JSON.parse(localStorage.getItem("cart"));

    cart.splice(i, 1);

    localStorage.setItem("cart", JSON.stringify(cart));

    loadCart();
    updateCartCount();
}

// Update cart count in navbar
function updateCartCount() {
    let cart = JSON.parse(localStorage.getItem("cart")) || [];

    let count = cart.reduce((sum, item) => sum + item.qty, 0);

    $("#cartCount").text(count);
}
   