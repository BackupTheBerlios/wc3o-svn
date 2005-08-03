var RS = new RemoteScripting();
function RemoteScripting()
{
	this.pool = new Array();
	this.poolSize = 0;
	this.maxPoolSize = 1000;
	this.usePOST = false;
	this.debug = false;

	// Sniff the browser
	if (document.layers)
		this.browser = "NS";
	else if (document.all)
	{
		var agent = navigator.userAgent.toLowerCase();
		if (agent.indexOf("opera") != -1)
			this.browser = "OPR";
		else if (agent.indexOf("konqueror") != -1)
			this.browser = "KONQ";
		else
			this.browser = "IE";
	}
	else if (document.getElementById)
		this.browser = "MOZ";
	else 
		this.browser = "OTHER";
}
RemoteScripting.prototype.Execute = function(url, method)
{
	var call = this.getAvailableCall();	
	var args = RemoteScripting.prototype.Execute.arguments;
	var len = RemoteScripting.prototype.Execute.arguments.length;

	var methodArgs = new Array();

	for (var i = 2; i < len; i++)
	{
		if (typeof(args[i]) == 'function')
		{
			call.callback = args[i++];		  
			
			if (i < len && typeof(args[i]) == 'function')
				call.callbackForError = args[i++];
			
			var ca = 0;
			for (; i < len; i++)
				call.callbackArgs[ca++] = args[i];
			break;
		}
		
		methodArgs[i - 2] = args[i];
	}
	
	call.showIfDebugging();

	if (this.usePOST && ((this.browser == 'IE') || (this.browser == 'MOZ')))
		call.POST(url, method, methodArgs);
	else 
		call.GET(url, method, methodArgs);
	
	return this.id;
}
RemoteScripting.prototype.PopupDebugInfo = function()
{
	var doc = window.open().document;
	doc.open();
	doc.write('<html><body>Pool Size: ' + this.poolSize + '<br><font face = "arial" size = "2"><b>');
	for (var i = 0; i < this.pool; i++)
	{
		var call = this.pool[i];
		doc.write('<hr>' + call.id + ' : ' + (call.busy ? 'busy' : 'available') + '<br>');
		doc.write(call.container.document.location.pathname + '<br>');
		doc.write(call.container.document.location.search + '<br>');
		doc.write('<table border = "1"><tr><td>' + call.container.document.body.innerHTML + '</td></tr></table>');
	}
	doc.write('</table></body></html>');
	doc.close();
	return false;
}
RemoteScripting.prototype.ReplaceOptions = function(element, optionsHTML)
{
	// Remove any existing options 
	while (element.options.length > 0)
        element.options[0] = null;

	// Create an array of each item for the dropdown
	var options = optionsHTML.split("</option>");
    var selectIndex = 0;
    var quote = (optionsHTML.indexOf("\"") > 0 ? "\"" : "'");

	// Fill 'er up
	for (var i = 0; i < options.length - 1; i++)
	{
	    aValueText = options[i].split(">");

	    option = new Option;
	    option.text = aValueText[aValueText.length - 1];
	    
	    // Account for the possibility of a value containing a >
	    for (var e = 1; e < aValueText.length - 1; e++)
			aValueText[0] += ">" + aValueText[e];

	    // Extract the value
	    var firstQuote = aValueText[0].indexOf(quote);
	    var lastQuote = aValueText[0].lastIndexOf(quote);
	    if (firstQuote > 0 && lastQuote > firstQuote + 1)
	        option.value = aValueText[0].substring(firstQuote + 1, lastQuote);

	    // Check if it's selected
	    if (aValueText[0].indexOf('selected') > 0)
	        selectIndex = i;

	    element.options[element.options.length] = option;
	}

	element.options[selectIndex].selected = true;
}
RemoteScripting.prototype.ReplaceOptions2 = function(optionsHTML, element)
{
	RS.ReplaceOptions(element, optionsHTML);
	window.status = "";	
}
RemoteScripting.prototype.getAvailableCall = function()
{
	for (var i = 0; i < this.poolSize; i++)
	{
		var call = this.pool['C' + (i + 1)];
		if (!call.busy)
		{
			call.busy = true;      
			return this.pool[call.id];
		}
	}
	
	// If we got here, there are no existing free calls
	if (this.poolSize <= this.maxPoolSize)
	{
		var callID = "C" + (this.poolSize + 1);
		this.pool[callID] = new RemoteScriptingCall(callID);
		this.poolSize++;
		return this.pool[callID];
	}

	alert("RemoteScripting Error: Call pool is full (no more than 1000 calls can be made simultaneously).");
	return null;
}
function RemoteScriptingCall(callID)
{
	this.id = callID;
	this.busy = true;
	this.callback = null;
	this.callbackForError = null;
	this.callbackArgs = new Array();

	switch (RS.browser)
	{
		case 'IE':
			document.body.insertAdjacentHTML("afterBegin", '<span id = "SPAN' + callID + '"></span>');
			this.span = document.all("SPAN" + callID);
			var html = '<iframe style = "width:800px" name = "' + callID + '" src = "./"></iframe>';
			this.span.innerHTML = html;
			this.span.style.display = 'none';
			this.container = window.frames[callID];
			break;
			
		case 'NS':
			this.container = new Layer(100);
			this.container.name = callID;
			this.container.visibility = 'hidden';
			this.container.clip.width = 100;
			this.container.clip.height = 100;
			break;
			
		case 'MOZ':
			this.span = document.createElement('SPAN');
			this.span.id = "SPAN" + callID;
			document.body.appendChild(this.span);
			var iframe = document.createElement('IFRAME');
			iframe.id = callID;
			iframe.name = callID;
			iframe.style.width = 800;
			iframe.style.height = 200;
			this.span.appendChild(iframe);
			this.container = iframe;
			break;
			
		case 'OPR':        
			this.span = document.createElement('SPAN');
			this.span.id = "SPAN" + callID;
			document.body.appendChild(this.span);
			var iframe = document.createElement('IFRAME');
			iframe.id = callID;
			iframe.name = callID;
			iframe.style.width = 800;
			iframe.style.height = 200;
			this.span.appendChild(iframe);
			this.container = iframe;
			break;
			
		case 'KONQ':  
		default:
			this.span = document.createElement('SPAN');
			this.span.id = "SPAN" + callID;
			document.body.appendChild(this.span);
			var iframe = document.createElement('IFRAME');
			iframe.id = callID;
			iframe.name = callID;
			iframe.style.width = 800;
			iframe.style.height = 200;
			this.span.appendChild(iframe);
			this.container = iframe;
			
			// Needs to be hidden for Konqueror, otherwise it'll appear on the page
			this.span.style.display = none;
			iframe.style.display = none;
			iframe.style.visibility = hidden;
			iframe.height = 0;
			iframe.width = 0;
	}	
}
RemoteScriptingCall.prototype.POST = function(url, method, args)
{
	var d = new Date();
	var unique = d.getTime() + '' + Math.floor(1000 * Math.random());
	var doc = (RS.browser == "IE") ? this.container.document : this.container.contentDocument;
	var paramSep = (url.lastIndexOf('?') < 0 ? '?' : '&');
	doc.open();
	doc.write('<html><body>');
	doc.write('<form name="rsForm" method="post" target=""');
	doc.write('action="' + url + paramSep + 'U=' + unique + '">');
	doc.write('<input type="hidden" name="RC" value="' + this.id + '">');
	
	// func and args are optional
	if (method != null)
	{
		doc.write('<input type = "hidden" name = "M" value = "' + method + '">');
		
		if (args != null)
		{
			if (typeof(args) == "string")
			{
				// single parameter
				doc.write('<input type = "hidden" name = "P0" '
					+ 'value = "[' + this.escapeParam(args) + ']">');
			}
			else 
			{
				// assume args is array of strings
				for (var i = 0; i < args.length; i++)
				{
					doc.write('<input type = "hidden" name = "P' + i + '" '
						+ 'value = "[' + this.escapeParam(args[i]) + ']">');
				}
			} // parm type
		} // args
	} // method
	
	doc.write('</form></body></html>');
	doc.close();
	doc.forms['rsForm'].submit();
}
RemoteScriptingCall.prototype.GET = function(url, method, args)
{
	// build URL to call
	var URL = url;
	var paramSep = (url.lastIndexOf('?') < 0 ? '?' : '&');
	
	// always send call
	URL += paramSep + "RC=" + this.id;
	
	// method and args are optional
	if (method != null)
	{
		URL += "&M=" + escape(method);
		
		if (args != null)
		{
			if (typeof(args) == "string")
			{
				// single parameter
				URL += "&P0=[" + escape(args + '') + "]";
			}
			else 
			{
				// assume args is array of strings
				for (var i = 0; i < args.length; i++)
				{
					URL += "&P" + i + "=[" + escape(args[i] + '') + "]";
				}
			} // parm type
		} // args
	} // method
	
	// unique string to defeat cache
	var d = new Date();
	URL += "&U=" + d.getTime();
	
	// make the call
	switch (RS.browser)
	{
		case 'IE':
			this.container.document.location.replace(URL);
			break;
		case 'NS':
			this.container.src = URL;
			break;
		case 'MOZ':
		case 'OPR':
		case 'KONQ':
		default:
			this.container.src = '';
			this.container.src = URL; 
			break;
	}  
}
RemoteScriptingCall.prototype.setResult = function(result)
{
	var argsCount = this.callbackArgs.length;

	if (result == true)
	{
		if (this.callback != null)
			this.callback(this.unescape(this.getPayload()), argsCount > 0 ? this.callbackArgs[0] : null, argsCount > 1 ? this.callbackArgs[1] : null, argsCount > 2 ? this.callbackArgs[2] : null);
	}
	else
	{
		if (this.callbackForError == null)
			alert(this.unescape(this.getPayload()));
		else
			this.callbackForError(this.unescape(this.getPayload()), argsCount > 0 ? this.callbackArgs[0] : null, argsCount > 1 ? this.callbackArgs[1] : null, argsCount > 2 ? this.callbackArgs[2] : null);
	}
		
	this.callback = null;
	this.callbackForError = null;
	this.callbackArgs = new Array();
	this.busy = false;	
}
RemoteScriptingCall.prototype.getPayload = function()
{
	switch (RS.browser)
	{
		case 'IE':
			return this.container.document.forms['rsForm']['rsPayload'].value;
		case 'NS':
			return this.container.document.forms['rsForm'].elements['rsPayload'].value;
		case 'MOZ':
			return window.frames[this.container.name].document.forms['rsForm']['rsPayload'].value; 
		case 'OPR':
			return window.frames[this.container.name].document.forms['rsForm']['rsPayload'].value; 
		case 'KONQ':
		default:
			return window.frames[this.container.name].document.getElementById("rsPayload").value;
	}  
}
RemoteScriptingCall.prototype.showIfDebugging = function()
{
	var vis = (RS.debug == true);
	switch (RS.browser)
	{
		case 'IE':
			document.all("SPAN" + this.id).style.display = (vis) ? '' : 'none';
			break;
		case 'NS':
			this.container.visibility = (vis) ? 'show' : 'hidden';
			break;
		case 'MOZ':
		case 'OPR':
		case 'KONQ':
		default:
			document.getElementById("SPAN" + this.id).style.visibility = (vis) ? '' : 'hidden';
			this.container.width = (vis) ? 250 : 0;
			this.container.height = (vis) ? 100 : 0;
			break; 
	}  
}
RemoteScriptingCall.prototype.escapeParam = function(str)
{
	return str.replace(/'"'/g, '\\"');
}
RemoteScriptingCall.prototype.unescape = function(str)
{
	return str.replace(/\\\//g, "/");
}
