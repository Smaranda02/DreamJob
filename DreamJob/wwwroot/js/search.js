
function searchItems() {
    const searchTerm = document.getElementById('searchBar').value.toLowerCase();
    const matches = document.querySelectorAll('.card');

    console.log(matches)


    matches.forEach(item => {

        const cardTitle = item.querySelector(' .card-body .card-title .name');

        console.log(cardTitle)

        if (cardTitle) {
            const name = cardTitle.textContent.toLowerCase();

            if (name.startsWith(searchTerm)) {
                item.style.display = '';
            } else {
                item.style.display = 'none';
            }
        }
    });
}
