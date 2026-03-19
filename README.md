# Piece Wear Tracking Manager (C# / Oracle)

## Overview
This project is a C# Windows Forms application developed for industrial use to manage tooling selection and piece wear tracking.

It helps operators and production systems:
- select the appropriate tooling setup for a work order
- monitor tooling usage counters
- compare current usage against maximum allowed lifetime
- store selection and increment history in Oracle
- support automatic or manual decision workflows

The application is designed for real shop-floor constraints, with a simple UI and direct integration with manufacturing data.

---

## Main Features
- Automatic or manual tooling selection
- Tooling lifetime tracking
- Usage counter increment
- Oracle database integration
- Selection history logging
- Support for work order / operation / machine context
- Filtering of tooling setups based on business rules

---

## Technologies Used
- C# (.NET Framework / Windows Forms)
- Oracle Database
- Oracle Managed Data Access
- Custom logging system

---

## Functional Workflow
1. The application receives context from the production environment:
   - work order
   - operation
   - article / part number
   - line
   - machine
   - operator
   - CDC
   - part revision
   - serial number list

2. It loads the eligible tooling setups from Oracle.

3. It checks:
   - existing selection for the current work order
   - current usage counter
   - maximum allowed lifetime
   - machine auto-selection mode

4. Depending on the result:
   - tooling can be selected automatically
   - or the operator can choose manually in the UI

5. The application:
   - inserts the selection if needed
   - increments the counter
   - stores the history in Oracle

---
## Database

This application relies on an Oracle database with tables related to:

- tooling setups / mountings

- usage counters and max lifetime

- selection history

- machine operating modes

Typical entities used by the project:

- mounting definitions

- lifetime counters

- work-order selections

- history / audit records


## Configuration

The application depends on external configuration and runtime context.

Typical configuration includes:

- Oracle connection settings

- logging paths

- machine / environment information

- production context passed from another system
  
## Industrial Use Case

This application was built in an industrial environment where tooling lifetime must be controlled to:

- reduce wear-related issues

- improve traceability

- standardize tooling selection

- ensure production consistency
