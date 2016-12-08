function getfinalUrl(baseurl, params) {
    var finalurl = ""+ baseurl;
    for (var i = 0; i < params.length; i++) {
        finalurl += '/' + params[i];
    }
    return finalurl;
}

function Do_GetCall(url, params, fun) {
    var finalurl = getfinalUrl(url, params);    
    $.getJSON(finalurl).done(fun);

}

function Do_PostCall(url, params, fun) {
    var finalUrl = getfinalUrl(url, params);
    $.post(finalUrl, { "data": json }, fun);
}
