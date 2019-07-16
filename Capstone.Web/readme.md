
***********************Documentation*****************************

* Made Models Based on The database
	* Weather
	* SurveyResult
	* Park

* Made DAL's to extract information from database
	* ParkDAL
		Methods
			* GetParks() - returns all Parks with the forecast list as a property

	* WeatherDAL
		* GetForecasts(string parkCode) - returns 5 day forecast for a particular park.