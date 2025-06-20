//$(document).on("click", ".detail .product-buttons .btn-dark", function (e) {
//    e.preventDefault(); // prevent <a href="">
//    let id = $(this).closest(".detail").data("id");
//    console.log("ID:", id);
//    $.ajax({
//        url: `/shop/addbasket?id=${id}`,
//        type: "POST",
//        success: function (res) {
//            let count = parseInt($(".basket-count").text()) || 0;
//            $(".basket-count").text(count + 1);
//        },
//        error: function (err) {
//            console.error("AddBasket Error:", err);
//        }
//    });
//});
$(document).on("click", ".detail .cart .btn-dark", function (e) {
    e.preventDefault();

    let id = $(this).closest(".product-summery").attr("data-id");
    let count = parseInt($(".basket-count").text());

    $.ajax({
        url: `/shop/addbasket?id=${id}`,
        type: "POST",
        success: function (res) {
            count++;
            $(".basket-count").text(count);
        },
        error: function (xhr) {
            if (xhr.status === 401) {
                // user login olmayıbsa login səhifəsinə yönləndir
                window.location.href = "/account/login";
            }
        }
    });
});