AppNet.NET
==========

AppNet.NET is a library which implements all endpoints and object from the App.Net API as documented at [http://developers.app.net/](http://developers.app.net/)

## Model ##

Find classes within the Model folder for every object type as documented by App.Net.
They are all in the `AppNetDotNet.Model` namespace (e. g. a post is of type `AppNetDotNet.Model.Post`

## API calls ##

All API calls as documented by App.Net are implemented as static functions in the `AppNetDotNet.ApiCalls` namespace.
For example to get the My Stream contents you call

	`AppNetDotNet.ApiCalls.getUserStream("AAAAccesstokenxxxxxxx")`

## Return values

All return values are implemented as as [Tuple](http://msdn.microsoft.com/en-us/library/system.tuple.aspx "Tuple") of the content plus an object of type [ApiCallResponse](https://github.com/liGhun/AppNet.NET/blob/master/AppNetDotNet/ApiCalls/ApiCallResponse.cs "ApiCallResponse"). This ApiCallResponse gives you infos about the `sucess` of the last call as well as the replied `rate_limits` and if an error occurs the reasons. It should never be null in any response - otherwise it would be a bug.

Example on how to get the My Stream of the current user:

	Tuple<List<Post>, ApiCallResponse> result;
	ParametersMyStream parameters = new ParametersMyStream();
	parameters.count = 100;
	parameters.include_annotations = true;
	result = AppNetDotNet.ApiCalls.SimpleStreams.getUserStream`("AccessTokenValue", parameters);
	if (result.Item2.success) {
		List<Post> posts = result.Item1
	}

## Auth ##

OAuth is implemented within the `AppNetDotNet.Model.AppNetAccount` class. This includes the full workflow including the used browser window asking the user for permission for the desktop flow.
The server side flow is not completed by now (I don't use .NET on webservers myself - so if you want to contribute...)

# Test tool #

There is a second project included with a (very simple) test window to test all API calls and to have examples of their usage. See MainWindow.xaml.cs and the other windows for all used API calls

Sometimes the result is rendered in the test window (e. g. the found places and the places search) but most of the time set a breakpoint to see what happens.

# Help #

Feel free to contact me @lighun any time if you questions or suggestions [https://alpha.app.net/lighun](https://alpha.app.net/lighun)