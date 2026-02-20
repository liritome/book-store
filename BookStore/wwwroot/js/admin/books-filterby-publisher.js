const publisherIds = document.getElementById("publisherIds");
const publisherInput = document.getElementById("publisherInput");
const publisherSearchResults = document.getElementById("publisherSearchResults");
const selectedPublishers = document.getElementById("selectedPublishers");

document.addEventListener("DOMContentLoaded", async function () {
    const publisherIdsItems = publisherIds.querySelectorAll(`input`);
    for (const item of publisherIdsItems) {
        const publisherResponse = await fetch(`/Admin/Publishers/SearchPublisherByIdJson?id=${encodeURIComponent(item.value)}`);
        const publisher = await publisherResponse.json();

        const element = createPublisherFilterElement(publisher, true)
        selectedPublishers.appendChild(element);
    }
});

document.getElementById("publisherSearchBtn").addEventListener("click", async () => {
    const query = publisherInput.value;
    const publishersResponse = await fetch(`/Admin/Publishers/SearchPublishersJson?query=${encodeURIComponent(query)}`);
    const publishers = await publishersResponse.json();
    publisherSearchResults.innerHTML = "";
    if (publishers.length == 0) {
        const element = document.createElement("span");
        element.className = "text-muted";
        element.innerText = "No publishers found";
        publisherSearchResults.appendChild(element);
    }
    publishers.forEach(publisher => {
        const element = createPublisherFilterElement(publisher, false);
        const isAlreadySelected = selectedPublishers.querySelector(`input[value="${publisher.id}"]`);
        publisherSearchResults.appendChild(element);
        if (isAlreadySelected) {
            element.hidden = true;
        }
    });
});

function createPublisherFilterElement(publisher, isChecked) {
    const publisherDiv = document.createElement("div");
    publisherDiv.className = "form-check";

    const publisherCheck = document.createElement("input");
    publisherCheck.type = "checkbox";
    publisherCheck.checked = isChecked;
    if (isChecked) {
        publisherCheck.name = "PublisherIds";
    }
    publisherCheck.className = "form-check-input";
    publisherCheck.value = publisher.id;
    const publisherId = `publisher${publisher.id}`;
    publisherCheck.id = publisherId;

    const publisherLabel = document.createElement("label");
    publisherLabel.className = "form-check-label";
    publisherLabel.textContent = publisher.name;
    publisherLabel.htmlFor = publisherId;

    publisherDiv.appendChild(publisherCheck);
    publisherDiv.appendChild(publisherLabel);

    publisherCheck.addEventListener("change", function () {
        const wrapper = this.closest(".form-check");

        if (this.checked) {
            wrapper.hidden = true;
            const checkedElement = createPublisherFilterElement(publisher, true)
            selectedPublishers.appendChild(checkedElement);
        }
        else {
            selectedPublishers.removeChild(wrapper);
            const searchedCheck = publisherSearchResults.querySelector(`input[value="${publisher.id}"]`);
            if (searchedCheck != null) {
                searchedCheck.checked = false;
                searchedCheck.closest(".form-check").hidden = false;
            }
        }
    });

    return publisherDiv;
}