function filterItems(input) {
    const query = input.value.toLowerCase();
    const items = input.closest(".input-group").nextElementSibling.querySelectorAll("label");

    items.forEach(item => {
        const checkbox = item.querySelector("input[type='checkbox']");
        const text = item.textContent.toLowerCase();

        item.style.display = (text.includes(query) || checkbox.checked) ? "" : "none";
    });
}

function clearSearch(button) {
    const input = button.closest(".input-group").querySelector("input");

    input.value = "";
    filterItems(input);
}