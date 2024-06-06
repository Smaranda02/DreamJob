
fetch('/Employer/GetJsonForUpdate', {
    method: 'GET',
})
    .then(response => response.json())
    .then(data => {
        manipulateData(data);
        console.log(data);
    })


function manipulateData(data) {
    document.getElementById('submitCareerField').addEventListener('click', function (e) {
        e.preventDefault();
        var careerFieldElements = document.querySelectorAll('#careerFieldContainer .careerField');
        var careerFields = [];

        careerFieldElements.forEach(function (careerFieldElement) {
            var careerFieldName = careerFieldElement.querySelector('.careerField-input').value;

            careerFields.push({
                CareerFieldName : careerFieldName
            });
        
        });



    var model = {
        Id: data.id,
        EmployerName: document.getElementById('employerName').value,
        EmployerDescription: document.getElementById('employerDescription').value,
        OfficeLocation: document.getElementById('officeLocation').value,
        CareerFields: careerFields,
        EmployerLinkedin: document.getElementById('employerLinkedin').value,
        Password: document.getElementById('employerPassword').value,
        Email : document.getElementById('employerMail').value
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

function editCareerField(button) {
    const tr = button.closest('tr');

    const isEditing = tr.classList.toggle('editing');
    const removeButton = tr.querySelector('.remove-careerField-button');

    const careerFieldSpan = tr.querySelector('.careerField-text');
    const careerFieldInput = tr.querySelector('.careerField-input'); // numele

    if (isEditing) {
        updateInput(careerFieldInput, 'inline-block', careerFieldSpan, 'none', 'text', isEditing);

        careerFieldSpan.style.display = 'none';
        careerFieldInput.type = 'text';

        removeButton.textContent = 'Cancel';
        removeButton.classList.remove('btn-danger');
        removeButton.classList.add('btn-secondary');

        removeButton.onclick = function () { cancelEditCareerField(tr) };
    }
    else {
        updateInput(careerFieldInput, 'none', careerFieldSpan, 'inline-block', 'text', isEditing);

        careerFieldSpan.textContent = careerFieldInput.value;
        careerFieldSpan.style.display = 'inline-block';

        removeButton.textContent = 'Remove';
        removeButton.classList.remove('btn-secondary');
        removeButton.classList.add('btn-danger');

        removeButton.onclick = function () { removeCareerField(tr) };
    }
    button.textContent = isEditing ? 'Save' : 'Edit';
    removeButton.textContent = isEditing ? 'Cancel' : 'Remove';
}

function addNewCareerFieldRow() {
    console.log('Aici se adauga un nou rand');

    const tableBody = document.getElementById('careerField-table-body');

    const newRow = document.createElement('tr');
    newRow.classList.add('careerField', 'editing');

    newRow.innerHTML = `
    <td>
        <span class="careerField-text" style="display:none;"></span>
        <input type="text" class="careerField-input form-control" value =""/>
    </td>

        <td>
            <button type="button" class="remove-careerField-button btn btn-secondary" onclick="cancelEditCareerField(this.closest('tr'))">Cancel</button>
            <button type="button" class="edit-carerrField btn btn-primary" onclick="saveNewCareerFieldRow(this)">Save</button>
        </td>
    `;
    tableBody.appendChild(newRow);
}





function cancelEditCareerField(tr) {

    // Exit edit mode without saving changes
    const careerFieldSpan = tr.querySelector('.careerField-text');
    const careerFieldInput = tr.querySelector('.careerField-input');

    careerFieldInput.type = 'hidden';
    careerFieldSpan.style.display = 'inline-block';
    careerFieldInput.style.display = 'none';

    // Change cancel button back to remove button
    const removeButton = tr.querySelector('.remove-careerField-button');
    removeButton.textContent = 'Remove';
    removeButton.classList.remove('btn-secondary');
    removeButton.classList.add('btn-danger');
    removeButton.onclick = function () { removeCareerField(tr); };

    // Change edit button text back to 'Edit'
    const editButton = tr.querySelector('.edit-careerField');
    editButton.textContent = 'Edit';

    // Remove the editing class
    tr.classList.remove('editing');
    removeCareerField(tr);
}



function removeCareerField(tr) {
    tr.remove();
}


function saveNewCareerFieldRow(button) {
    const tr = button.closest('tr');

    const careerFieldInput = tr.querySelector('.careerField-input');

    if(!careerFieldInput.value){
        return;
    }

    const careerFieldSpan = tr.querySelector('.careerField-text');
    careerFieldSpan.textContent = careerFieldInput.value;

    careerFieldSpan.style.display = 'inline-block';
    careerFieldInput.style.display = 'none';

    const removeButton = tr.querySelector('.remove-careerField-button');
    removeButton.textContent = 'Remove';
    removeButton.classList.remove('btn-secondary');
    removeButton.classList.add('btn-danger');

    removeButton.onclick = function () { removeCareerField(tr) };

    button.textContent = 'Edit';
    button.onclick = function () { editCareerField(button) };

    tr.classList.remove('editing');
}

function postData(model) {
    console.log(JSON.stringify(model));
    fetch('/Employer/Update', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(model)
    })
        .then(response => response.json())
        .then(res => {
            if (res && res.errors && res.errors.length > 0) {
                createDiv.insertBefore(errorsDiv, form);
                res.errors.forEach(error => {
                    newError = document.createElement('div');
                    newError.classList.add('text-danger');
                    newError.textContent = error.errorMessage;
                    errorsDiv.appendChild(newError);
                });
            } else {
                location.reload();
            }
        }
    )
}



