function deleteJobOffer(id) {

    var confirmation = window.confirm("Are you sure you want to delete this job offer?");

    if (confirmation) {

            fetch(`/JobOffer/Delete?id=${id}`, {
                method: "post",


            }).then(location.reload())
        }
}