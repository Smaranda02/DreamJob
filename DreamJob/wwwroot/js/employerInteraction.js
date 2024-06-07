var candidates = candidatesData || []

function likeCandidateClicked(index) {


    console.log(candidates[index]);
    var date = new Date().toISOString;

    var interaction = {
        CandidateId: candidates[index].Id,
        JobOfferId: candidates[index].JobOffer.Id,
        InteractionDate: date,
        FeedbackCandidate: true,
        FeedbackEmployer: true,
    };

    postData(interaction);

}


function dislikeCandidateClicked(index) {

    console.log(candidates[index]);
    var currentDate = new Date();
    var date = currentDate.toISOString();

    var interaction = {
        CandidateId: candidates[index].Id,
        JobOfferId: candidates[index].JobOffer.Id,
        InteractionDate: date,
        FeedbackCandidate: true,
        FeedbackEmployer: false,
    };

    postData(interaction);

}


function postData(interaction) {

    fetch(`/Interaction/CreateUpdate/${true}`, {
        method: "post",
        body: JSON.stringify(interaction),
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