let skip = 0;
const take = 3;

async function loadProducts() {
    try {
        const response = await fetch(`/Product/LoadMore?skip=${skip}&take=${take}`);
        const data = await response.json();

        if (data.success) {
            const container = document.getElementById("productsContainer");

            if (data.products.length === 0) {
                document.getElementById("loadMoreBtn").style.display = "none";
                return;
            }

            data.products.forEach(p => {
                const html = `
                        <div class="col-md-4 mb-4">
                            <div class="card">
                                <img src="/images/${p.image}" class="card-img-top" alt="${p.name}">
                                <div class="card-body">
                                    <h5 class="card-title">${p.name}</h5>
                                    <p class="card-text">${p.description}</p>
                                    <p><strong>Price:</strong> $${p.price}</p>
                                    <p><strong>Category:</strong> ${p.categoryName}</p>
                                    <p><strong>Brand:</strong> ${p.brandName}</p>
                                </div>
                            </div>
                        </div>`;
                container.insertAdjacentHTML('beforeend', html);
            });

            skip += take;
        } else {
            alert("Error: " + data.message);
        }
    } catch (err) {
        console.error("Error loading products:", err);
    }
}

// İlk yükləmə
document.addEventListener("DOMContentLoaded", loadProducts);

// "More" düyməsinə klikdə
document.getElementById("loadMoreBtn").addEventListener("click", function (e) {
    e.preventDefault();
    loadProducts();
});

document.getElementById('sortSelect').addEventListener('change', function () {
    var sortKey = this.value;

    fetch(`/Shop/GetSortedProducts?sortKey=${sortKey}`)
        .then(res => res.json())
        .then(data => {
            var container = document.getElementById('shop-products');
            container.innerHTML = '';

            data.forEach(item => {
                var html = `
        <div class="grid-item col featured">
            <div class="product">
                <div class="product-thumb">
                    <a href="/Shop/Detail/${item.id}" class="image">
                        <img src="/assets/img/product-details/${item.image}" alt="Product Image" />
                        <img class="image-hover" src="/assets/img/product-details/${item.hower}" alt="Product Image" />
                    </a>
                    <a href="wishlist.html" class="add-to-wishlist hintT-left" data-hint="Add to wishlist">
                        <i class="far fa-heart"></i>
                    </a>
                </div>
                <div class="product-info">
                    <h6 class="title">
                        <a href="/Shop/Detail/${item.id}">${item.name}</a>
                    </h6>
                    <span class="price">$${item.price}</span>
                    <div class="product-buttons">
                        <a href="/ProductDetails/Index" class="product-button hintT-top" data-hint="View">
                            <i class="fas fa-search"></i>
                        </a>
                        <a href="#" class="product-button hintT-top" data-hint="Add to Cart">
                            <i class="fas fa-shopping-cart"></i>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        `;
                container.insertAdjacentHTML('beforeend', html);
            });

        });
});



const priceSlider = document.getElementById("priceSlider");
const priceValue = document.getElementById("priceValue");

priceSlider.addEventListener("input", () => {
    const minPrice = priceSlider.value;
    priceValue.textContent = minPrice;

    fetch(`/Shop/FilterByPrice?min=${minPrice}&max=10000`)
        .then(res => res.json())
        .then(data => {
            const container = document.getElementById("shop-products");
            container.innerHTML = "";

            if (data.length === 0) {
                container.innerHTML = "<p>No products found in this range.</p>";
                return;
            }

            data.forEach(item => {
                const html = `
                        <div class="grid-item col featured">
                            <div class="product">
                                <div class="product-thumb">
                                    <a href="/Shop/Detail/${item.id}" class="image">
                                        <img src="/assets/img/product-details/${item.image}" alt="${item.name}" />
                                        <img class="image-hover" src="/assets/img/product-details/${item.hower}" alt="${item.name}" />
                                    </a>
                                    <a href="#" class="add-to-wishlist hintT-left" data-hint="Add to wishlist">
                                        <i class="far fa-heart"></i>
                                    </a>
                                </div>
                                <div class="product-info">
                                    <h6 class="title">
                                        <a href="/Shop/Detail/${item.id}">${item.name}</a>
                                    </h6>
                                    <span class="price">$${item.price}</span>
                                    <div class="product-buttons">
                                        <a href="/Shop/Detail/${item.id}" class="product-button hintT-top" data-hint="View">
                                            <i class="fas fa-search"></i>
                                        </a>
                                        <a href="#" class="product-button hintT-top" data-hint="Add to Cart">
                                            <i class="fas fa-shopping-cart"></i>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    `;
                container.insertAdjacentHTML("beforeend", html);
            });
        })
        .catch(err => console.error("Error:", err));
});




