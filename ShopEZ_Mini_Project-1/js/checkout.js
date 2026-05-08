
// When checkout form is submitted
$("#checkoutForm").submit(function (e) {

    e.preventDefault(); // stop page reload

    alert("Order placed successfully!");

    // Clear cart after order
    localStorage.removeItem("cart");
});
