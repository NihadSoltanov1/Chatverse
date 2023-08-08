@model GetBlogViewModel

    < h1 >@Model.Title</h1 >
<p>@Model.Content</p>

<h2>Yorumlar</h2>
<ul id="commentList">
    @foreach (var comment in Model.Comments)
    {
        <li>@comment.Content</li>
    }
</ul>

<form id="commentForm">
    <input type="text" id="commentInput" placeholder="Yorumunuzu ekleyin">
    <button type="submit">Yorum Ekle</button>
</form>

@section Scripts {
    <script>
        $(() => {
            var connection = new signalR.HubConnectionBuilder().withUrl("/commentHub").build();
        connection.start();

        connection.on("LoadComment", function () {
               
            // Yeni yorum eklenince yapılacak işlemler
            // Örneğin, yorumları güncelleyerek sayfayı yenilemeye gerek kalmadan gösterebilirsiniz
            $.ajax({
                url: 'http://localhost:5273/api/Users/GetAuthorPosts/',
                method: 'GET',
                success: function (posts) {
                    document.querySelectorAll('.showComments').forEach(item => {
                        item.addEventListener('click', e => {
                            var targetElement = e.target;
                            console.log(targetElement);
                            if (targetElement.tagName === 'I' || targetElement.tagName === 'SPAN') {
                                targetElement = $(targetElement).closest('a')[0];
                            }

                            // Yalnızca <a> etiketine tıklanıldığında burası çalışır
                            if (targetElement.tagName === 'A') {
                                console.log('Sadece <a> etiketine tıklandı.');
                            }
                            const commentId = targetElement.getAttribute('id');
                            console.log(commentId);
                            var commentListId = 'comment' + commentId
                            var commentElement = document.getElementById(commentListId);
                            commentElement.style.display = 'block';
                        })
                    })

                    $("#commentList").empty();
                    blog.comments.forEach(function (comment) {
                        var newComment = "<li>" + comment.content + "</li>";
                        $("#commentList").append(newComment);
                    });
                },
                error: function (error) {
                    console.error("Error loading blog: ", error);
                }
            });
                
            });

        $("#commentForm").submit(function (e) {
            e.preventDefault();
        var commentId = $("#PostId").val();
        var commentContent = $("#commentContent").val();

        $.ajax({
            url: 'http://localhost:5273/api/Comments/CreateComment',
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            PostId: commentId,
        Content: commentContent
                    }),
        success: function () {
            // Yorum eklendikten sonra inputu temizle
            $("#commentContent").val("");
                    },
        error: function (error) {
            console.error("Error adding comment: ", error);
                    }
                });
            });
        });
    </script>
}
