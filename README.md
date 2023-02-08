# Waiter_availability_app

#Waiters Page
![Screenshot from 2023-02-07 15-18-57](https://user-images.githubusercontent.com/95750574/217255887-88e963f1-b925-42d0-867f-012784839014.png)

#What waiters can do:
* Enter their name on the URL to identify themselves to bookdays
* select days they can work
* update the days they can work on

#Manager Screen 
![Screenshot from 2023-02-07 15-19-09](https://user-images.githubusercontent.com/95750574/217256809-ac69e561-cb1c-4e59-8fa4-eb458f05a3a9.png)

#Manager can :
* see how many waiters are available to work
* reset the data to use the system for a new week

#To Access Application Use: 
* waitersapp.zezethu.projectcodex.net
********************************************************

#How to run localy 

* Git Clone the application from github by running the command : git clone https://github.com/ZezethuM/waiter_availability_app
* Change into the right folder : cd WaitersRazorPages
* Compile and run the app using : dotnet restore
                                  dotnet build  -c Release
                                  dotnet bin/Release/net6.0/WaitersRazorPages.dll --urls=http://localhost:6007/
* Things you might need to install : dotnet sdk
- to Install checkout: https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu



