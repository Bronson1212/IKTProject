
function showHideComments()
{
    var commetsAndButtons = document.getElementById("commentsAndButtons")

    //check for state
    if (commetsAndButtons)
    {

    }
}


function SendComment(messageId) {
    //get the comment message
    var commentMessage = document.getElementById("comment").value;

    //check if the comment message is empty
    if (commentMessage == "" || commentMessage == null) {
        return;
    }

    //get url
    var url = '@Url.Action("PublishComment")';

    //get antiforgerytoken
    var t = document.getElementsByName("__RequestVerificationToken")[0].value;
    console.log(t)

    $.ajax({
        type: 'POST',
        url: url,
        data: {

            comment: commentMessage,
            messageId: messageId,
            __RequestVerificationToken: t,

        },
        success: successCallback,
        dataType: "json",
        error: function (xhr, status, errorThrown) {
            var err = "Status: " + status + " " + errorThrown;
            console.log(err);
        },

        // headers:
        // {
        //     "__RequestVerificationToken": t
        // },

    });

    //empty the message input
    document.getElementById("comment").value = "";
}

function successCallback(response) {
    console.log(response)
    if (response.status == "good") { // add comment

        var commentsDiv = document.getElementById("allComents")
        commentsDiv.innerHTML = "<p> " + response.commText + " </p><br/>" + commentsDiv.innerHTML;
    }
}
