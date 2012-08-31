Twitter Connect API - ASP.Net MVC3 C#
================

This is Twitter API example in ASP.Net MVC3 C#

Using this example, ASP.Net MVC developer can get source code of 
1) Twitter API, 
2) Login with  Twitter 
3) Pull user's twitter account's information
3) pull users twitter Follwers list
4) tweet message on twitter account
5) Send direct message from user's twitter account


Follow below Steps to make this application run:
1) Download this source code.
2) Please add reference three dll files[TweetSharp.dll,Hammock.ClientProfile.dll,Newtonsoft.Json.dll ] in your project
3) Create application from your twitter developer account [Note: only live url is valid for this application, so please set live DomainName & callback url(http://YOUR_DOMAIN_NAME/home/returnfromtwitter) & Set applicatins Access as "Read, Write & Access direct messages" from Settings tab] and put Consumer key and Consumer Secret key in web.config file.
4) Please add this code in web.config file in <assemblyBinding> tag:
	 <dependentAssembly>
	        <assemblyIdentity name="Newtonsoft.Json" publicKeyToken="30ad4fe6b2a6aeed" culture="neutral" />
        	<bindingRedirect oldVersion="0.0.0.0-4.5.0.0" newVersion="4.5.0.0" />
      </dependentAssembly>

5)Build and then test the project.

If you want to test this source code on live then visit link: http://twittertest.emiprotechnologies.com/


We are waiting for your comments. Feel free to contact me for further query.

Best luck,
Dilip from Emipro Technologies,
dilip@emiprotechnologies.com
http://www.emiprotechnologies.coms/
