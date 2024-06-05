

    fetch(`/Candidate/GetJsonForUpdate`, {
        method: "get",
    })
        .then(response => response.json())
        .then(data => {

            manipulateData(data);
            console.log(data);

        })

function manipulateData(data) {


    document.getElementById("submitCandidate").addEventListener("click", function (e) {
        e.preventDefault();

        var studies = [];
        var studyElements = document.querySelectorAll('#studyContainer .study');

        studyElements.forEach(function (studyElement) {
            var university = studyElement.querySelector('.university-input').value;
            var specialty = studyElement.querySelector('.specialty-input').value;
            var startYear = studyElement.querySelector('.startYear-input').value;
            var endYear = studyElement.querySelector('.endYear-input').value;

            var startDate = `${startYear}-01-01T00:00:00`;
            var endDate = `${endYear}-01-01T00:00:00`;


            studies.push({
                University: university,
                Specialty: specialty,
                StartYear: startDate,
                EndYear: endDate,
            });
        });


        var experiences = [];
        var experienceElements = document.querySelectorAll('#workContainer .work');

        experienceElements.forEach(function (experienceElement) {
            var name = experienceElement.querySelector('.name-input').value;
            var description = experienceElement.querySelector('.description-input').value;
            var startYear = experienceElement.querySelector('.startYear-input').value;
            var endYear = experienceElement.querySelector('.endYear-input').value;

            var startDate = `${startYear}-01-01T00:00:00`;
            var endDate = `${endYear}-01-01T00:00:00`;


            experiences.push({
                ExperienceName: name,
                ExperienceDescription: description,
                StartYear: startDate,
                EndYear: endDate,
            });
        });

        var model = {
            Id : data.id,
            FirstName: document.getElementById("firstname").value,
            Surname: document.getElementById("surname").value,
            CandidateDescription: document.getElementById("description").value,
            Password: document.getElementById("password").value,
            Linkedin: document.getElementById("linkedin").value,
            Email: document.getElementById("email").value,
            Studies: studies,
            Experiences : experiences
        }

        console.log(model);

        postData(model);
    });
}

function updateInput(input, inputStyle, span, spanStyle, type, isEditing) {

    input.type = type;

    if (isEditing) {
        input.value = span.textContent.trim();
    }
    else {
        span.textContent = input.value;
    }

    span.style.display = spanStyle;
    input.style.display = inputStyle;

}

function editStudy(button) {
    // Get the parent <tr> element
    const tr = button.closest('tr');

    const isEditing = tr.classList.toggle('editing');

    const removeButton = tr.querySelector('.remove-study-button');

    const universitySpan = tr.querySelector('.university-text');
    const universityInput = tr.querySelector('.university-input');

    const specialtySpan = tr.querySelector('.specialty-text');
    const specialtyInput = tr.querySelector('.specialty-input');

    const yearsSpan = tr.querySelector('.years-text');
    const startYearInput = tr.querySelector('.startYear-input');
    const endYearInput = tr.querySelector('.endYear-input');

    if (isEditing) {
        // Entering edit mode
        updateInput(universityInput, 'inline-block', universitySpan, 'none', 'text', isEditing);
        updateInput(specialtyInput, 'inline-block', specialtySpan, 'none', 'text', isEditing);

        yearsSpan.style.display = 'none';
        startYearInput.type = 'text';
        endYearInput.type = 'text';
        startYearInput.style.display = 'inline-block';
        endYearInput.style.display = 'inline-block';

        removeButton.textContent = "Cancel";
        removeButton.classList.remove('btn-danger');
        removeButton.classList.add('btn-secondary');
        removeButton.onclick = function () { cancelEditStudy(tr); };

    } else {
        // Exiting edit mode
        updateInput(universityInput, 'none', universitySpan, 'inline-block', 'hidden', isEditing);
        updateInput(specialtyInput, 'none', specialtySpan, 'inline-block', 'hidden', isEditing);

        yearsSpan.textContent = `${startYearInput.value} - ${endYearInput.value}`;
        yearsSpan.style.display = 'inline-block';
        startYearInput.type = 'hidden';
        endYearInput.type = 'hidden';
        startYearInput.style.display = 'none';
        endYearInput.style.display = 'none';


        // Change cancel button back to remove button
        removeButton.textContent = 'Remove';
        removeButton.classList.remove('btn-secondary');
        removeButton.classList.add('btn-danger');
        removeButton.onclick = function () { removeStudy(tr); };
    }

    // Change button text to 'Save' if in edit mode
    button.textContent = isEditing ? 'Save' : 'Edit';
    removeButton.textContent = isEditing ? "Cancel" : "Remove";
}


function editWork(button) {
    // Get the parent <tr> element
    const tr = button.closest('tr');

    const isEditing = tr.classList.toggle('editing');

    const removeButton = tr.querySelector('.remove-work-button');

    const nameSpan = tr.querySelector('.name-text');
    const nameInput = tr.querySelector('.name-input');

    const descriptionSpan = tr.querySelector('.description-text');
    const descriptionInput = tr.querySelector('.description-input');

    const yearsSpan = tr.querySelector('.years-text');
    const startYearInput = tr.querySelector('.startYear-input');
    const endYearInput = tr.querySelector('.endYear-input');

    if (isEditing) {
        // Entering edit mode
        updateInput(nameInput, 'inline-block', nameSpan, 'none', 'text', isEditing);
        updateInput(descriptionInput, 'inline-block', descriptionSpan, 'none', 'text', isEditing);

        yearsSpan.style.display = 'none';
        startYearInput.type = 'text';
        endYearInput.type = 'text';
        startYearInput.style.display = 'inline-block';
        endYearInput.style.display = 'inline-block';

        removeButton.textContent = "Cancel";
        removeButton.classList.remove('btn-danger');
        removeButton.classList.add('btn-secondary');
        removeButton.onclick = function () { cancelEditExperience(tr); };

    } else {
        // Exiting edit mode
        updateInput(nameInput, 'none', nameSpan, 'inline-block', 'hidden', isEditing);
        updateInput(descriptionInput, 'none', descriptionSpan, 'inline-block', 'hidden', isEditing);

        yearsSpan.textContent = `${startYearInput.value} - ${endYearInput.value}`;
        yearsSpan.style.display = 'inline-block';
        startYearInput.type = 'hidden';
        endYearInput.type = 'hidden';
        startYearInput.style.display = 'none';
        endYearInput.style.display = 'none';


        // Change cancel button back to remove button
        removeButton.textContent = 'Remove';
        removeButton.classList.remove('btn-secondary');
        removeButton.classList.add('btn-danger');
        removeButton.onclick = function () { removeWork(tr); };
    }

    // Change button text to 'Save' if in edit mode
    button.textContent = isEditing ? 'Save' : 'Edit';
    removeButton.textContent = isEditing ? "Cancel" : "Remove";
}



function cancelEditStudy(tr) {

    // Exit edit mode without saving changes
    const universitySpan = tr.querySelector('.university-text');
    const universityInput = tr.querySelector('.university-input');

    const specialtySpan = tr.querySelector('.specialty-text');
    const specialtyInput = tr.querySelector('.specialty-input');

    const yearsSpan = tr.querySelector('.years-text');
    const startYearInput = tr.querySelector('.startYear-input');
    const endYearInput = tr.querySelector('.endYear-input');

    universityInput.type = 'hidden';
    universitySpan.style.display = 'inline-block';
    universityInput.style.display = 'none';

    specialtyInput.type = 'hidden';
    specialtySpan.style.display = 'inline-block';
    specialtyInput.style.display = 'none';

    yearsSpan.style.display = 'inline-block';
    startYearInput.type = 'hidden';
    endYearInput.type = 'hidden';
    startYearInput.style.display = 'none';
    endYearInput.style.display = 'none';

    // Change cancel button back to remove button
    const removeButton = tr.querySelector('.remove-study-button');
    removeButton.textContent = 'Remove';
    removeButton.classList.remove('btn-secondary');
    removeButton.classList.add('btn-danger');
    removeButton.onclick = function () { removeStudy(tr); };

    // Change edit button text back to 'Edit'
    const editButton = tr.querySelector('.edit-study');
    editButton.textContent = 'Edit';

    // Remove the editing class
    tr.classList.remove('editing');
    removeStudy(tr);
}


function cancelEditExperience(tr) {

    // Exit edit mode without saving changes
    const nameSpan = tr.querySelector('.name-text');
    const nameInput = tr.querySelector('.name-input');

    const descriptionSpan = tr.querySelector('.description-text');
    const descriptionInput = tr.querySelector('.description-input');

    const yearsSpan = tr.querySelector('.years-text');
    const startYearInput = tr.querySelector('.startYear-input');
    const endYearInput = tr.querySelector('.endYear-input');

    nameInput.type = 'hidden';
    nameSpan.style.display = 'inline-block';
    nameInput.style.display = 'none';

    descriptionInput.type = 'hidden';
    descriptionSpan.style.display = 'inline-block';
    descriptionInput.style.display = 'none';

    yearsSpan.style.display = 'inline-block';
    startYearInput.type = 'hidden';
    endYearInput.type = 'hidden';
    startYearInput.style.display = 'none';
    endYearInput.style.display = 'none';

    // Change cancel button back to remove button
    const removeButton = tr.querySelector('.remove-work-button');
    removeButton.textContent = 'Remove';
    removeButton.classList.remove('btn-secondary');
    removeButton.classList.add('btn-danger');
    removeButton.onclick = function () { removeWork(tr); };

    // Change edit button text back to 'Edit'
    const editButton = tr.querySelector('.edit-work');
    editButton.textContent = 'Edit';

    // Remove the editing class
    tr.classList.remove('editing');
    removeWork(tr);
}

function removeStudy(tr) {
    tr.remove();
}

function removeWork(tr) {
    tr.remove();
}

function addNewExperienceRow() {

    const workTableBody = document.getElementById('work-table-body');

    const newWorkRow = document.createElement('tr');
    newWorkRow.classList.add('work', 'editing');

    newWorkRow.innerHTML = `
        <td>
            <span class="name-text" style="display: none;"></span>
            <input type="text" class="name-input form-control" value="" />
        </td>
        <td>
            <span class="description-text" style="display: none;"></span>
            <input type="text" class="description-input form-control" value="" />
        </td>
        <td>
            <span class="years-text" style="display: none;"></span>
            <input type="text" class="startYear-input form-control" value="" />
            <input type="text" class="endYear-input form-control" value="" />
        </td>
        <td>
            <button type="button" class="remove-work-button btn btn-secondary" onclick="cancelEditExperience(this.closest('tr'))">Cancel</button>
            <button type="button" class="edit-work btn btn-primary" onclick="saveNewExperienceRow(this)">Save</button>
        </td>
    `;

    workTableBody.appendChild(newWorkRow);
}


function addNewStudyRow() {


    const tableBody = document.getElementById('studies-table-body');
    // Create a new row
    const newRow = document.createElement('tr');
    newRow.classList.add('study', 'editing');

    newRow.innerHTML = `
        <td>
            <span class="university-text" style="display: none;"></span>
            <input type="text" class="university-input form-control" value="" />
        </td>
        <td>
            <span class="specialty-text" style="display: none;"></span>
            <input type="text" class="specialty-input form-control" value="" />
        </td>
        <td>
            <span class="years-text" style="display: none;"></span>
            <input type="text" class="startYear-input form-control" value="" />
            <input type="text" class="endYear-input form-control" value="" />
        </td>
        <td>
            <button type="button" class="remove-study-button btn btn-secondary" onclick="cancelEditStudy(this.closest('tr'))">Cancel</button>
            <button type="button" class="edit-study btn btn-primary" onclick="saveNewStudyRow(this)">Save</button>
        </td>
    `;

    tableBody.appendChild(newRow);
}

function saveNewStudyRow(button) {
    const tr = button.closest('tr');

    // Get all input elements within the row
    const universityInput = tr.querySelector('.university-input');
    const specialtyInput = tr.querySelector('.specialty-input');
    const startYearInput = tr.querySelector('.startYear-input');
    const endYearInput = tr.querySelector('.endYear-input');

    // Validate inputs (optional)
    if (!universityInput.value || !specialtyInput.value || !startYearInput.value || !endYearInput.value) {
        alert('All fields are required.');
        return;
    }

    // Get all span elements within the row
    const universitySpan = tr.querySelector('.university-text');
    const specialtySpan = tr.querySelector('.specialty-text');
    const yearsSpan = tr.querySelector('.years-text');

    // Set span text to input values
    universitySpan.textContent = universityInput.value;
    specialtySpan.textContent = specialtyInput.value;
    yearsSpan.textContent = `${startYearInput.value} - ${endYearInput.value}`;

    // Hide input fields and show spans
    universitySpan.style.display = 'inline-block';
    specialtySpan.style.display = 'inline-block';
    yearsSpan.style.display = 'inline-block';

    universityInput.style.display = 'none';
    specialtyInput.style.display = 'none';
    startYearInput.style.display = 'none';
    endYearInput.style.display = 'none';

    // Change buttons back to edit/remove
    const removeButton = tr.querySelector('.remove-study-button');
    removeButton.textContent = 'Remove';
    removeButton.classList.remove('btn-secondary');
    removeButton.classList.add('btn-danger');
    removeButton.onclick = function () { removeStudy(tr); };

    button.textContent = 'Edit';
    button.onclick = function () { editStudy(button); };

    // Remove the editing class
    tr.classList.remove('editing');
}



function saveNewExperienceRow(button) {
    const tr = button.closest('tr');

    // Get all input elements within the row
    const nameInput = tr.querySelector('.name-input');
    const descriptionInput = tr.querySelector('.description-input');
    const startYearInput = tr.querySelector('.startYear-input');
    const endYearInput = tr.querySelector('.endYear-input');

    // Validate inputs (optional)
    if (!nameInput.value || !descriptionInput.value || !startYearInput.value || !endYearInput.value) {
        alert('All fields are required.');
        return;
    }

    // Get all span elements within the row
    const nameSpan = tr.querySelector('.name-text');
    const descriptionSpan = tr.querySelector('.description-text');
    const yearsSpan = tr.querySelector('.years-text');

    // Set span text to input values
    nameSpan.textContent = nameInput.value;
    descriptionSpan.textContent = descriptionInput.value;
    yearsSpan.textContent = `${startYearInput.value} - ${endYearInput.value}`;

    // Hide input fields and show spans
    nameSpan.style.display = 'inline-block';
    descriptionSpan.style.display = 'inline-block';
    yearsSpan.style.display = 'inline-block';

    nameInput.style.display = 'none';
    descriptionInput.style.display = 'none';
    startYearInput.style.display = 'none';
    endYearInput.style.display = 'none';

    // Change buttons back to edit/remove
    const removeButton = tr.querySelector('.remove-work-button');
    removeButton.textContent = 'Remove';
    removeButton.classList.remove('btn-secondary');
    removeButton.classList.add('btn-danger');
    removeButton.onclick = function () { removeWork(tr); };

    button.textContent = 'Edit';
    button.onclick = function () { editWork(button); };

    // Remove the editing class
    tr.classList.remove('editing');
}



function postData(model) {
    console.log(JSON.stringify(model));
    fetch(`/Candidate/Update`, {
        method: "post",
        body: JSON.stringify(model),
        headers: {
            "Content-Type": "application/json"
        }
    })
        .then(response => response.json())
        .then(res => {
            if (res && res.errors && res.errors.length > 0) {

                createDiv.insertBefore(errorsDiv, form);
                res.errors.forEach(err => {
                    newError = document.createElement("div");
                    newError.classList.add("text-danger");
                    newError.textContent = err.errorMessage;
                    errorsDiv.appendChild(newError);
                });

            } else {
                location.reload();
            }
        });
}
