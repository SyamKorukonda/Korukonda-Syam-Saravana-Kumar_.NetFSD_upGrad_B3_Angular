const products=[
     { name: "Laptop", price: 68000, quantity: 1 },
    { name: "Mouse", price: 675, quantity: 2 },
    { name: "Keyboard", price: 1894, quantity: 1 }
];
const invoiceLines = products.map(product =>
    `${product.name} - ₹${product.price} x ${product.quantity} = ₹${product.price * product.quantity}`
);
const totalAmount = products.reduce((total, product) =>
    total + (product.price * product.quantity), 0
);
const invoice = `
=========== SHOPPING CART ===========
${invoiceLines.join("\n")}

-------------------------------------
Total Amount: ₹${totalAmount}
=====================================
`;
document.getElementById("invoice").textContent = invoice;