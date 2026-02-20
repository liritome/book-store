const authorIds = document.getElementById("authorIds");
const authorInput = document.getElementById("authorInput");
const authorSearchResults = document.getElementById("authorSearchResults");
const selectedAuthors = document.getElementById("selectedAuthors");

document.addEventListener("DOMContentLoaded", async function () {
    const authorIdsItems = authorIds.querySelectorAll(`input`);
    for (const item of authorIdsItems) {
        const authorResponse = await fetch(`/Admin/Authors/SearchAuthorByIdJson?id=${encodeURIComponent(item.value)}`);
        const author = await authorResponse.json();

        const element = createAuthorFilterElement(author, true)
        selectedAuthors.appendChild(element);
    }
});

document.getElementById("authorSearchBtn").addEventListener("click", async () => {
    const query = authorInput.value;
    const authorsResponse = await fetch(`/Admin/Authors/SearchAuthorsJson?query=${encodeURIComponent(query)}`);
    const authors = await authorsResponse.json();
    authorSearchResults.innerHTML = "";
    if (authors.length == 0) {
        const element = document.createElement("span");
        element.className = "text-muted";
        element.innerText = "No authors found";
        authorSearchResults.appendChild(element);
    }
    authors.forEach(author => {
        const element = createAuthorFilterElement(author, false);
        const isAlreadySelected = selectedAuthors.querySelector(`input[value="${author.id}"]`);
        authorSearchResults.appendChild(element);
        if (isAlreadySelected) {
            element.hidden = true;
        }
    });
});

function createAuthorFilterElement(author, isChecked) {
    const authorDiv = document.createElement("div");
    authorDiv.className = "form-check";

    const authorCheck = document.createElement("input");
    authorCheck.type = "checkbox";
    authorCheck.checked = isChecked;
    if (isChecked) {
        authorCheck.name = "AuthorIds";
    }
    authorCheck.className = "form-check-input";
    authorCheck.value = author.id;
    const authorId = `author${author.id}`;
    authorCheck.id = authorId;

    const authorLabel = document.createElement("label");
    authorLabel.className = "form-check-label";
    authorLabel.textContent = author.name;
    authorLabel.htmlFor = authorId;

    authorDiv.appendChild(authorCheck);
    authorDiv.appendChild(authorLabel);

    authorCheck.addEventListener("change", function () {
        const wrapper = this.closest(".form-check");

        if (this.checked) {
            wrapper.hidden = true;
            const checkedElement = createAuthorFilterElement(author, true)
            selectedAuthors.appendChild(checkedElement);
        }
        else {
            selectedAuthors.removeChild(wrapper);
            const searchedCheck = authorSearchResults.querySelector(`input[value="${author.id}"]`);
            if (searchedCheck != null) {
                searchedCheck.checked = false;
                searchedCheck.closest(".form-check").hidden = false;
            }
        }
    });

    return authorDiv;
}