# Dephion "Phone book" Assignment
The "Phone book" project was a technical assignment created for Dephion. Its aim was to assess my technical knowledge. 
## Technical Description
The project was developed using C# on the Unity Engine 2020.3.0f1 version, as it is the latest fully released 
version of the engine. The app was tested and working in portrait mode on an Android or a Windows Desktop device.
### Extra Plugins
For the animations in the project it was used the [DoTween plugin](http://dotween.demigiant.com/) 
and for persitency it was used the [Microsoft LiveOps service- Playfab](https://playfab.com/).
### Dependencies
* Internet- The project uses non-flat persistent storage system- Playfab. Constant access to the internet 
is required, so that the database can be updated successfully. 
## How to run?
You can run the app/game from:
* The Unity Editor with version 2020.3.0f1.
* Build the game on an Android device.
* Build the game on a Windows Desktop device with the following Player Settings:
  * Fullscreen mode: windowed mode.
  * resolution 1080 x 1920
## About the progam
In the "Phone book" app you can: 
* Add a contact, by his first/last name, phone number, description, email and twitter.
* Easily find a contact by his first name via a search field.
* Group your contacts alphabetically or by date of adding the in your phone book.
* Assign fun images provided from the app, to each of your contacts.
* Create your own profile/contact data, so that later on when the app expands share it with your fellow friends. 
## Cool technical stuff
With the aim to make the app as optimized and flexible as possible:
* MVC pattern was used to handle the UI.
* SOLID principles were taken consideration and used when applicable, so that the app can reuse its code as much as possible and allow maintainability and flexability.
* A non-flat percisency storage system was used- Playfab. All the data of the game is stored in Playfab and in JSON format.
  * Each player/user has its own player data, which is constantly synced with the data shown in the game/app.
  * The developers can track when the player opens and how the player uses the app, from the Playfab console, within their browser.
  * The developers can dynamically change the Title data of the game/app from the Playfab console, within their browser.
  * Every update you make to the Title data, the next time the user starts the app/game, he will see the new changes, 
as the app gets the data from the server.
* UI Pooling technique was used in order to handle the data in the scroll views. 
  * As a lot of data to present within a scroll view can result in slowing the CPU of the device and
 making a lot of work for the garbage collector, the scroll views- "my contacts" and "select profile image" are using
 this UI pooling technique, which reuses already created and rendered fields, to present new data- In order to see the code, which achieves that please refer to
 "DephionAssignment/DephionAssignment/Assets/Scripts/UI/Pooling/PoolController.cs"
  * The pooling script is done in a way to be reused on different scroll views with different styles and prefabs.
* The user can add fun images for profile or for each of his contacts.
## Class design decisions
* Each created contact has a unique 'guid' string, so that it can be used if the app expands more and wants to do more with this system.
* With the view to order the contacts by their date of creation, each contact stores the date of its creation in a 'long' type (DateTime.UtcNow.Ticks).
If this time is going to be compared with other devices, then it has to change to take the time from the server and not take the time with: DateTime.UtcNow.Ticks.
* While researching in what data type you should store a phone number, it appeared that phone numbers can have letters in some countries, that is why it was decided to store the
phone number as a string as the rest of the data.
### Early design
![Webp net-resizeimage (2)](https://user-images.githubusercontent.com/41620452/121266934-abb2e680-c8bb-11eb-9c14-f668b7336e8e.jpg) ![Webp net-resizeimage (3)](https://user-images.githubusercontent.com/41620452/121267057-e157cf80-c8bb-11eb-9ff7-839a84f8bfb1.jpg)

## Questions
For further questions about the app, please refer to its codebase, where almost all functions are commented and explained. For more information, please [contact me](lyudmilpashayanov.com). 
