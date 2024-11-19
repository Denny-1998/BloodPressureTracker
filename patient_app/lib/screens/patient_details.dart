// patient_details_page.dart

import 'package:flutter/material.dart';

class PatientDetails extends StatelessWidget {
  final Map<String, dynamic> patientData;

  PatientDetails({required this.patientData});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Patient Details"),
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text("SSN: ${patientData['ssn']}", style: TextStyle(fontSize: 18)),
            SizedBox(height: 8),
            Text("Name: ${patientData['name']}",
                style: TextStyle(fontSize: 18)),
            SizedBox(height: 8),
            Text("Email: ${patientData['email']}",
                style: TextStyle(fontSize: 18)),
          ],
        ),
      ),
    );
  }
}
