# FullStackSample

Design a vehicle management system that for MVP will only cater for cars, but in the future 
will manage other vehicle types like boats, bikes, caravans etc.

The project implements basic CRUD functionality presented via a list of cars and a modal
popup for create/update.

The front-end project is based upon:
* React as the base technology
* Chakra-UI provides the controls
* Formik provides form processing 
* Emotion-JS provides CSS-in-JS 

To run,
1. Start the API
2. "nm install" the site
3. "npm start" the site

If there are reference problems with the site, the following packages are required:
npm install @chakra-ui/core @emotion/core @emotion/styled emotion-theming
npm install react-icons
npm install formik --save


The back-end project is a stock .Net Core API utilising a SQLite database to show perisstence.

