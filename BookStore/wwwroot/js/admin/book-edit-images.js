const container = document.getElementById("image-container");
let draggedItem = null;

container.addEventListener("dragstart", e => {
    draggedItem = e.target.closest(".image-item");
    draggedItem.classList.add("opacity-50");
});

container.addEventListener("dragend", () => {
    draggedItem.classList.remove("opacity-50");
    draggedItem = null;
    updateOrderNumbers();
});

container.addEventListener("dragover", e => {
    e.preventDefault();
    const target = e.target.closest(".image-item");
    if (!target || target === draggedItem) return;

    const rect = target.getBoundingClientRect();
    const next = (e.clientX - rect.left) > rect.width / 2;

    container.insertBefore(draggedItem, next ? target.nextSibling : target);
});

document.querySelectorAll(".image-delete-btn").forEach(btn => {
    btn.addEventListener("click", (e) => {
        const item = e.target.closest(".image-item");
        const deleteInput = item.querySelector('input[name$=".Delete"]');
        deleteInput.value = "true";
        item.style.display = "none";
        updateOrderNumbers();
    });
});

function updateOrderNumbers() {
    const items = container.querySelectorAll(".image-item");
    let visibleIndex = 0;
    items.forEach(item => {
        const order = item.querySelector('input[name$=".Order"]');
        const deleteInput = item.querySelector('input[name$=".Delete"]');
        if (deleteInput.value == "false") {
            order.value = visibleIndex++;
        }
    });
}