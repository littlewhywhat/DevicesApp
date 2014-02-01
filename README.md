MiaApp
======

DataBase App

  Aim of this app to have an option of structuring information about different devices. This information is represented
by DataItems. There are four kinds - Devices, DeviceTypes, DeviceEvents, Companies. Main dataitems are Devices that have 
references to other types. DeviceTypes can specify structure and common info of devices to make easier the way of filling 
information. Company is just a type of reference for devices to some common info that doesn't affect on other devices 
info.
DeviceEvents represent collections of dataItems on which device can refer.
  You can build trees of devices, specifying their product numbers, types and companies. you can also add events that 
happened to devices. There is a search textbox where you can write any property of dataItems to find any of them.
