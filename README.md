AppNet.NET
==========

AppNet.NET is a library which implements all endpoints and object from the App.Net API as documented at https://github.com/appdotnet/api-spec

## Model ##

Find classes within the Model folder for every object type as documented at https://github.com/appdotnet/api-spec/blob/master/objects.md

## API calls ##

All APi as documented under https://github.com/appdotnet/api-spec/tree/master/resources are implemented as static functions in the ApiCalls class and subclasses

## Auth ##

OAuth is implemented within the Model/AppNetAccount class. This includes the full workflow including the used browser window asking the user for permission

# Test tool #

There is a second project included with a (very simple) test window to test all API calls and to have examples of their usage. See MainWindow.xaml.cs for all used API calls