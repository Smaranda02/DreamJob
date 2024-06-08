
function searchItems() {
    const searchTerm = document.getElementById('searchBar').value.toLowerCase();
    const matches = document.querySelectorAll('.card');

    console.log(matches)


    matches.forEach(item => {

        var cardTitle = item.querySelector(' .card-body .card-title .name');

        if (cardTitle) {
            var name = cardTitle.textContent.toLowerCase();
            console.log(searchTerm);
            console.log(name)
            if (name.startsWith(searchTerm)) {
                item.style.display = '';
            } else {
                item.style.display = 'none';
            }
        }
    });
}
