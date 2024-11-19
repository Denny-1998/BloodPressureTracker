// home_screen.dart

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'dart:convert'; // Don't forget to import for jsonEncode
import 'package:http/http.dart' as http; // Import the HTTP package
import '../services/rabbitmq_service.dart';
import 'patient_details.dart'; // Import the new file for the patient details screen

class HomeScreen extends StatefulWidget {
  final RabbitmqService rabbitMQService = RabbitmqService();

  HomeScreen({Key? key}) : super(key: key);

  @override
  _HomeScreenState createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  final TextEditingController _systolicController = TextEditingController();
  final TextEditingController _diastolicController = TextEditingController();
  final TextEditingController _patientSSNController = TextEditingController();
  final TextEditingController _patientIdController =
      TextEditingController(); // New controller for patient ID

  Future<void> _sendMessage() async {
    final measurement = {
      "dateTime": DateTime.now().toIso8601String(), // Current timestamp
      "systolic": int.tryParse(_systolicController.text) ?? 0,
      "diastolic": int.tryParse(_diastolicController.text) ?? 0,
      "seen": false,
      "patientSSN": _patientSSNController.text.isEmpty
          ? "Unknown"
          : _patientSSNController.text,
    };

    final measurementJson = jsonEncode(measurement);
    print(measurementJson);

    await widget.rabbitMQService.connect(measurementJson);
  }

  Future<void> _fetchPatientData() async {
    final patientId = _patientIdController.text;

    // Send GET request to the API with the patient ID
    final response = await http
        .get(Uri.parse('https://localhost:5003/api/Patient/$patientId'));

    if (response.statusCode == 200) {
      // If the request is successful, parse the data and navigate to the new page
      final Map<String, dynamic> patientData = jsonDecode(response.body);

      // Navigate to the PatientDetailsPage and pass the data
      Navigator.push(
        context,
        MaterialPageRoute(
          builder: (context) => PatientDetails(patientData: patientData),
        ),
      );
    } else {
      // If the request fails, show an error message
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Failed to load patient data')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Blood Pressure Measurement"),
      ),
      body: Center(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              // Blood pressure fields
              TextField(
                controller: _systolicController,
                decoration: InputDecoration(
                  labelText: "Systolic",
                  border: OutlineInputBorder(),
                ),
                keyboardType: TextInputType.number,
              ),
              SizedBox(height: 16),
              TextField(
                controller: _diastolicController,
                decoration: InputDecoration(
                  labelText: "Diastolic",
                  border: OutlineInputBorder(),
                ),
                keyboardType: TextInputType.number,
              ),
              SizedBox(height: 16),
              TextField(
                controller: _patientSSNController,
                decoration: InputDecoration(
                  labelText: "Patient SSN",
                  border: OutlineInputBorder(),
                ),
                keyboardType: TextInputType.text,
              ),
              SizedBox(height: 32),
              ElevatedButton(
                onPressed: () async {
                  await _sendMessage();
                },
                child: Text("Send Message"),
              ),
              SizedBox(height: 32),
              // New Patient ID field
              TextField(
                controller: _patientIdController,
                decoration: InputDecoration(
                  labelText: "Patient ID",
                  border: OutlineInputBorder(),
                ),
                keyboardType: TextInputType.number,
              ),
              SizedBox(height: 16),
              ElevatedButton(
                onPressed: () async {
                  await _fetchPatientData();
                },
                child: Text("Fetch Patient Data"),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
