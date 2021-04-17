# api-reqres-tests

This repository contains Autoamtion code for API testing with HttpClient C#, Nunit and FluentAssertion.


How to Run the test cases:

Using Visual studio IDE:

1. Clone the Repository and Get the latest
2. In Visual Studio, Go to Test-> Test Settings -> Select Test Settings File -> Select test.runsettings file under solution Item folder
3. Build and Run

Using Command lines:

1. Open Developer Command Prompt to use the command-line tool, or you can find the tool in %Program Files(x86)%\Microsoft Visual Studio\<version>\<edition>\common7\ide\CommonExtensions\<Platform | Microsoft>\TestWindow
2. Type VSTest.Console In.Reqres.Api.Tests.dll /Settings:test.runsettings 
