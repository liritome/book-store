async function initSingleSelectInput(config) {
    const entityName = config.entityName;
    const fieldName = config.fieldName;
    const searchByQueryUrl = config.searchByQueryUrl;
    const searchByIdUrl = config.searchByIdUrl;

    const idsContainer = document.getElementById(`${entityName}Ids`);
    const input = document.getElementById(`${entityName}Input`);
    const searchBtn = document.getElementById(`${entityName}SearchBtn`);
    const selectedContainer = document.getElementById(`${entityName}Selected`);
    const resultsContainer = document.getElementById(`${entityName}SearchResults`);

    const idItems = idsContainer.querySelectorAll(`input`);
    for (const item of idItems) {
        const response = await fetch(`${searchByIdUrl}?id=${encodeURIComponent(item.value)}`);
        const entity = await response.json();
        const element = createElement(entity, true)
        selectedContainer.appendChild(element);
    }

    searchBtn.addEventListener("click", async () => {
        const query = input.value;
        const response = await fetch(`${searchByQueryUrl}?query=${encodeURIComponent(query)}`);
        const entities = await response.json();
        resultsContainer.innerHTML = "";
        if (entities.length == 0) {
            const element = document.createElement("span");
            element.className = "text-muted";
            element.innerText = `No ${entityName}s found`;
            resultsContainer.appendChild(element);
        }
        entities.forEach(entity => {
            const element = createElement(entity, false);
            const isAlreadySelected = selectedContainer.querySelector(`input[value="${entity.id}"]`);
            resultsContainer.appendChild(element);
            if (isAlreadySelected) {
                element.hidden = true;
            }
        });
    });

    function createElement(entity, isChecked) {
        const wrapper = document.createElement("div");
        wrapper.className = "form-check";

        const checkbox = document.createElement("input");
        checkbox.type = "checkbox";
        checkbox.checked = isChecked;
        if (isChecked) {
            checkbox.name = fieldName;
        }
        checkbox.className = "form-check-input";
        checkbox.value = entity.id;
        const entityId = `${entityName}${entity.id}`;
        checkbox.id = entityId;

        const label = document.createElement("label");
        label.className = "form-check-label";
        label.textContent = entity.name;
        label.htmlFor = entityId;

        wrapper.appendChild(checkbox);
        wrapper.appendChild(label);

        checkbox.addEventListener("change", function () {
            const wrapper = this.closest(".form-check");
            if (this.checked) {
                const previouslySelected = selectedContainer.querySelector(`input`);
                if (previouslySelected != null) {
                    const previousWrapper = previouslySelected.closest(`.form-check`);
                    selectedContainer.removeChild(previousWrapper);
                    const previousSearchedCheck = resultsContainer.querySelector(`input[value="${previouslySelected.value}"]`);
                    if (previousSearchedCheck != null) {
                        previousSearchedCheck.checked = false;
                        previousSearchedCheck.closest(".form-check").hidden = false;
                    }
                }
                wrapper.hidden = true;
                const selectedElement = createElement(entity, true)
                selectedContainer.appendChild(selectedElement);
            }
            else {
                selectedContainer.removeChild(wrapper);
                const searchedCheck = resultsContainer.querySelector(`input[value="${entity.id}"]`);
                if (searchedCheck != null) {
                    searchedCheck.checked = false;
                    searchedCheck.closest(".form-check").hidden = false;
                }
            }
        });

        return wrapper;
    }
}
