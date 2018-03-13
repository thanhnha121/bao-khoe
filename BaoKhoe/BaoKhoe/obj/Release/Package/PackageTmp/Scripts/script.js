function loadMoreCategory(e) {
    var listArticles = $('.article');
    var categoryId = $('#id-category-div').attr('data-id');

    var listUrls = [];
    for (var i = 0; i < listArticles.length; i++) {
        var tmp = listArticles.eq(i).attr('data-url');
        if (tmp != undefined && tmp !== '') {
            listUrls.push(tmp);
        }
    }
    try {
        $.ajax({
            url: 'Load-More-Category',
            type: 'post',
            dataType: 'json',
            success: function (data) {
                console.log(data);
                $("#btn-load-more-category").remove();
                $("#load-more-category").replaceWith(data);
            },
            error: function (data) {
                $("#btn-load-more-category").remove();
                $("#load-more-category").replaceWith(data.responseText);
            },
            data: {
                categoryId: categoryId - 0,
                listUrls: listUrls.toString()
            }
        });
    } catch (error) {
        console.log(error);
    }

}

function loadMoreTag(e) {
    var listArticles = $('.article');

    var listUrls = [];
    for (var i = 0; i < listArticles.length; i++) {
        var tmp = listArticles.eq(i).attr('data-url');
        if (tmp != undefined && tmp !== '') {
            listUrls.push(tmp);
        }
    }
    try {
        $.ajax({
            url: '/Load-More-Tag',
            type: 'post',
            dataType: 'json',
            success: function (data) {
                console.log(data);
                $("#btn-load-more-tag").remove();
                $("#load-more-tag").replaceWith(data);
            },
            error: function (data) {
                $("#btn-load-more-tag").remove();
                $("#load-more-tag").replaceWith(data.responseText);
            },
            data: {
                listUrls: listUrls.toString(),
                tag: $('#tag-input-data').attr('data-tag-input') ? $('#tag-input-data').attr('data-tag-input').trim() : ''
            }
        });
    } catch (error) {
        console.log(error);
    }
}

function loadMoreSearch(searchInput) {
    var listArticles = $('.article');

    var listUrls = [];
    for (var i = 0; i < listArticles.length; i++) {
        var tmp = listArticles.eq(i).attr('data-url');
        if (tmp != undefined && tmp !== '') {
            listUrls.push(tmp);
        }
    }
    try {
        $.ajax({
            url: '/Load-More-Search',
            type: 'post',
            dataType: 'json',
            success: function (data) {
                console.log(data);
                $("#btn-load-more-search").remove();
                $("#load-more-search").replaceWith(data);
            },
            error: function (data) {
                $("#btn-load-more-search").remove();
                $("#load-more-search").replaceWith(data.responseText);
            },
            data: {
                searchInput: searchInput,
                listUrls: listUrls.toString()
            }
        });
    } catch (error) {
        console.log(error);
    }
}

function searchClick() {
    var searchInput = $('#search-input').val();
    if (searchInput != undefined && searchInput.trim() !== '') {
        window.location.href = "/tim-kiem/" + searchInput.trim().replace(/ /g, "-");
    }
}

$(document).ready(function () {
    $("#search-input").keyup(function (e) {
        if (e.which === 13) {
            searchClick();
        }
    });

    $('#balloon_wrap').click(() => {
        $("html, body").animate({ scrollTop: 0 }, "slow");
    });
});
