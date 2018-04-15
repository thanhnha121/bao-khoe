$(document).ready(function () {
  $('#open_sub').click(function () {
        $('body').toggleClass("active");
  });
});

function onExpandMenuClick(e) {
  $(e.target).parent('li').find('.expand-ul-sub-cate').toggle('show');
  if ($(e.target).html().trim() === '+') {
    $(e.target).html('-');
  } else {
    $(e.target).html('+');
  }
}

var getIndexPartial = () => {
  var list = $('.article-class');
  var listIds = [];
  for (var i = list.length - 30; i < list.length; i++) {
    if (i >= 0) {
      listIds.push(list.eq(i).attr('data-id') - 0);
    }
  }
  $.ajax({
    url: '/Home/IndexPartial',
    type: "POST",
    data: { listIds: listIds },
    success: function (res) {
      $('#index-partial').replaceWith(res);
    },
    error: function (error) {
      console.log(error);
    }
  });
};

var getDetailIndexPartial = (url) => {
  $.ajax({
    url: '/Detail/IndexPartial',
    type: "POST",
    data: { url: url },
    success: function (res) {
      $('#index-partial').replaceWith(res);
    },
    error: function (error) {
      console.log(error);
    }
  });
};