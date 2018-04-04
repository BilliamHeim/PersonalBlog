$(document).ready(function () {
    $('#submitButton').on("click", function () {
        search();
    });
});

function search() {
    var contentRows = $('.contentRows');
    var searchCat = document.getElementById("Categories")
    searchCat = searchCat.options[searchCat.selectedIndex].text;
    var searchTag = document.getElementById("searchTag");
    searchTag = searchTag.value;
    if (searchTag == "") {
        searchTag = "emptytag";
    }
    contentRows.children('.row').remove();
    $.ajax({
        type: 'GET',
        url: 'http://localhost:51170/search/' + searchCat + "/" + searchTag,
        success: function (itemArray) {
            $.each(itemArray, function (index, item) {
                postId = item.id;
                postTitle = item.PostTitle;
                postBody = item.PostBody;

                var row = '<div class="row"><div class="col-md-12 text-center"><h1>';
                row += postTitle;
                row += '</h1 ></div ></div>';
                row += '<div class="row"><div class="col-md-12 text-center"><p>';
                row += postBody;
                row += '</p ></div></div>';
                contentRows.append(row);
            });
        },
        error: function () {
            $('#errorMessage')
                .append($('<li>')
                    .attr({
                        class: 'list-group-item list-group-item-danger'
                    })
                    .text('Error calling web service, please try again later'));
        }
    });
}