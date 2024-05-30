

    fetch(`/Candidate/GetJsonForUpdate`, {
        method: "get",
    })
        .then(response => response.json())
        .then(data => {

            manipulateData(data);
            console.log(data);

        })

function manipulateData(data) {

    var studies = [];
    var studyElements = document.querySelectorAll('#studyContainer .study');

    studyElements.forEach(function (studyElement) {
        var university = studyElement.querySelector('.university').value;
        var specialty = studyElement.querySelector('.specialty').value;
        var startYear = studyElement.querySelector('.startYear').value;
        var endYear = studyElement.querySelector('.endYear').value;

        // Print each study element's data to the console
        console.log('University:', university);
        console.log('Specialty:', specialty);
        console.log('Start Year:', startYear);
        console.log('End Year:', endYear);

        studies.push({
            University: university,
            Specialty: specialty,
            StartYear: parseInt(startYear),
            EndYear: parseInt(endYear),
            CandidateId : data.id
        });
    });

    // Print the entire studies array to the console
    console.log('Studies:', studies);


    var model = {
        FirstName : document.getElementById("firstname"),
        Surname : document.getElementById("surname"),
        CandidateDescription : document.getElementById("description"),
        Password : document.getElementById("password"),
        Linkedin : document.getElementById("linkedin"),
        Studies :  studies
    }

    console.log(model);


    document.getElementById("submitCandidate").addEventListener("click", function () {
        postData(model);
    });

}

function postData(model) {

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

    //var educationButton = document.getElementById("add-education-button");
    //var studyContainer = document.getElementById("studyContainer");
    ////var cancelStudies = document.getElementById("cancel-add-studies");

    //educationButton.addEventListener("click", function () {
    //    studyContainer.style.display = "block";
    //});

    //var form = document.getElementById("updateCandidate");

    //cancelStudies.addEventListener("click", function () {
    //    studyContainer.style.display = "none";
    //})

    //var saveStudies = document.getElementById("save-add-studies");

    //saveStudies.addEventListener("click", function () {

    //})
