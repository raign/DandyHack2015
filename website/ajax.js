function doAction(action, params, altUrl) {
    var url = actionsUrl;
    if (arguments.length == 3) {
        url = altUrl;
    }
    var request = {
        type: 'POST',
        url: url,
        dataType: 'json'
    };
    if (typeof FormData === 'function' && params instanceof FormData) {
        request.data = params;
        request.url += '?action=' + encodeURIComponent(action);
        request.cache = false;
        request.contentType = false;
        request.processData = false;
    } else if (params instanceof Array) {
        params.push({
            name: 'action',
            value: action
        });
        request.data = params;
    } else {
        params.action = action;
        request.data = params;
    }
    // return the Deferred so that code can .done() and .fail() it
    return $.ajax(request);
}