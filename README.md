---
# Start Application
You can run the application in two ways, and for both ways you must have the .Net 8 SDK installed. 
- The first way is to run the application in the Visual Studio/Rider IDE, to do this, open the Crypto.App folder in the program and run the code.
- The second way is to install the application on your PC using the "SetupFile.msi" installer from the repository, after installing the application you will see a shortcut on your desktop. **Appendix:** I have not been able to test the installer on other versions of the operating system, so I am not sure about the full functionality of this application installer.

---
## Completed tasks
For this Testing task, I use the CoinGecko API 

1. The main page displays the top N currencies by popularity in a certain market (or the top 10 currencies returned by the API). **Appendix:** to return to the main page, click on Cryptocurency App.
![MainPage](https://github.com/user-attachments/assets/7f97fb5c-2360-423f-9587-2289e0df6c2e)

2. Page with the ability to view detailed information about the currency: price, volume, price change, in which markets it can be purchased and at what price (the ability to go to the currency page on the market is a plus).
![DetailsCurrency](https://github.com/user-attachments/assets/6f67a10e-c370-45e8-8240-c45f1111bbb6)

3. Searching for currency by name or code.
![Serching](https://github.com/user-attachments/assets/5092722f-490b-4a85-a30d-b881a96450f1)

4. I also use the MVVM pattern, I tried to make a good UI/UX design and make the code clean, you can see all the code in the Crypto.App folder. 

---
