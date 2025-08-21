# Weather Application

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Project Structure](#project-structure)
- [Installation](#installation)
- [Usage](#usage)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)
- [Flowchart](#flowchart)
- [Video](#video)
- [License](#license)

## Overview
The Weather Application is a user-friendly desktop application built using the Windows Forms framework in C#. It provides real-time weather information and 30-day weather forecasts for cities worldwide. Users can select a city from a dropdown list, view current weather conditions, and navigate to a detailed forecast page. The application fetches data from the OpenWeather API and displays it in an intuitive interface.

## Features
- **City Selection**: Choose from a ComboBox populated with over 1,000 city names loaded from a CSV file.
- **Current Weather Display**: View real-time weather details including temperature, description, humidity, and more.
- **30-Day Forecast**: Navigate to a forecast page displaying weather predictions for the selected city over the next 30 days.
- **Loading Indicator**: Visual feedback during API calls to enhance user experience.
- **Error Handling**: Graceful handling of invalid inputs or API errors.

## Project Structure
- **Frontend**:
  - ComboBox for city selection (populated from a CSV file).
  - Search button to trigger weather data retrieval.
  - Display area for current weather details.
  - Navigation button to access the 30-day forecast page.
- **Backend**:
  - Asynchronous API calls to OpenWeather API using C# (HttpClient).
  - CSV file parsing for city list.
  - JSON response parsing for weather data using Newtonsoft.Json or System.Text.Json.
- **Data Flow**:
  1. User selects a city from the ComboBox.
  2. Clicking the Search button triggers an async method.
  3. A loading state is displayed.
  4. The app fetches data from the OpenWeather API.
  5. Parsed data updates the UI with current weather.
  6. Users can navigate to the forecast page for 30-day predictions.
  7. Errors are handled and displayed to the user.

## Installation
1. **Clone the Repository**:
   ```bash
   git clone <repository-url>
   ```
2. **Open in Visual Studio**:
   - Open the solution file (`.sln`) in Visual Studio.
   - Ensure you have the .NET Framework installed (compatible with Windows Forms).
3. **Add OpenWeather API Key**:
   - Sign up at [OpenWeather](https://openweathermap.org/) to obtain an API key.
   - Add the API key to your application configuration (e.g., `App.config`):
     ```xml
     <appSettings>
       <add key="OpenWeatherApiKey" value="your_api_key_here"/>
     </appSettings>
     ```
4. **Prepare City Data**:
   - The `1000Cities.csv` file is included in the `data` folder of the repository. Ensure it remains in the project directory (e.g., alongside the executable in `bin/Debug` or `bin/Release`).
   - The CSV file has the following structure (first row is header): `rank,Latitude,Longitude,City,Country,2021 Population,2020 Population,Growth,Population Difference,Population Change`.
   - The application parses the "City" column to populate the ComboBox. Use the following code in your Form's Load event to load the cities:
     ```csharp
     using System;
     using System.Collections.Generic;
     using System.IO;
     using System.Windows.Forms;

     private void Form1_Load(object sender, EventArgs e)
     {
         string csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/1000Cities.csv");
         var cities = new List<string>();

         try
         {
             using (var reader = new StreamReader(csvPath))
             {
                 reader.ReadLine(); // Skip header row
                 while (!reader.EndOfStream)
                 {
                     var line = reader.ReadLine();
                     var values = line.Split(',');
                     if (values.Length >= 4) // City is in index 3
                     {
                         cities.Add(values[3]);
                     }
                 }
             }
             ComboBox1.Items.AddRange(cities.ToArray());
         }
         catch (Exception ex)
         {
             MessageBox.Show("Error loading city data: " + ex.Message);
         }
     }
     ```
     If You Want My Data Download From: https://drive.google.com/file/d/1B-KioIJETpu1JRpxvAPQyftWC0Bq_Y8O/view?usp=drive_link


## Usage
1. **Run the Application**:
   - Build and run the solution in Visual Studio (`F5` or Debug > Start Debugging).
2. **Select a City**:
   - Use the ComboBox to choose a city from the list.
3. **View Current Weather**:
   - Click the Search button to fetch and display current weather data.
4. **Access Forecast**:
   - Click the Forecast button to navigate to the 30-day weather forecast page.
5. **Error Handling**:
   - If a city is invalid or the API fails, an error message will be displayed.

## Technologies Used
- **Frontend**: Windows Forms (C#) for the user interface.
- **Backend**: C# for API calls and data processing.
- **API**: OpenWeather API for weather data.
- **Data**: CSV file for city list.
- **Libraries**: HttpClient for API requests, Newtonsoft.Json or System.Text.Json for JSON parsing.

## Contributing
Contributions are welcome! Please follow these steps:
1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -m "Add feature"`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a pull request.

## Flowchart
The following flowchart illustrates the data flow and user interaction within the Weather Application:
<img width="2211" height="844" alt="WeatherAppliction drawio" src="https://github.com/user-attachments/assets/0a718d5e-0b2f-4b47-ae67-a650f6b8a6c8" /> 



## Video
View a demonstration of the Weather Application, showcasing city selection, current weather display, and forecast navigation:

Download Video From Drive: 
https://drive.google.com/file/d/18ggFELNaVi3TBxy5cp_-5U5nNZDOpvB6/view?usp=drive_link
 

## License
This project is licensed under the MIT License.ADME (3).markdown…]()
