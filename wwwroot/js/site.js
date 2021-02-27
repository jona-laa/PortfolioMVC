$(function () {
    $('#edit-items').on('click', '.pager a', function () {
        var url = $(this).attr('href');
        $('#edit-items').load(url);
        $(document).scrollTop($("main").offset().top);
        return false;
    })
});