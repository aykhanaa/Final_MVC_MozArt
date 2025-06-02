"use strict";

let deleteCategoryBtns = document.querySelectorAll(".delete-category");

deleteCategoryBtns.forEach((btn)=> {
    btn.addEventListener("click", function () {
        let id = this.getAttribute("data-id");
        let deletedElem = this.parentNode.parentNode;

        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {

                deleteCategory(id, deletedElem); 
                Swal.fire({
                    title: "Deleted!",
                    text: "Your file has been deleted.",
                    icon: "success",
                    showConfirmButton: false,
                    timer:1000
                });
            }
        });
    })
})
async function deleteCategory(id,elem) {
    const response = await fetch(`/admin/category/delete?id=${id}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
    });

    elem.remove();
}


