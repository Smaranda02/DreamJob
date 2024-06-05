var jobOffers = jobOffersData || []

var interactions = []

function likeClicked(index) {
       
    console.log(jobOffers[index]);
    var currentDate = new Date();
    var date = currentDate.toISOString();

    var interaction = {
        CandidateId: 1,
        JobOfferId: jobOffers[index].Id,
        InteractionDate: date,
        FeedbackCandidate: true,
        FeedbackEmployer: false,
    };

    //console.log(interaction);

    //interactions.push(interaction);


    fetch(`/Interaction/CreateUpdate/${false}`, {
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
