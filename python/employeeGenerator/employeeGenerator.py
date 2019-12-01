#!/usr/bin/env python3
""" Script to generate employees """

# Add the utilities folder to the search path
from pathlib import Path
import os
path = os.path.abspath(__file__)
folder = os.path.dirname(path)
utilitiesFolder = Path(folder).parent.joinpath("utilities")

import sys
sys.path.append(str(utilitiesFolder.resolve()))


import requests
import random
import utilities
import json


class Employee:

    currentEmployeeId = 0

    def __init__(self, names, role, supervisor):
        self.id = Employee.currentEmployeeId
        Employee.currentEmployeeId += 1

        self.names = names
        self.role = role

        if (supervisor != None):
            self.supervisorId = supervisor.id

    def fullName(self) -> str:
        return " ".join(self.names)


def createNames() -> [str]:
    url = "https://uinames.com/api/"
    r = requests.get(url=url)
    result = r.json()
    return [result["name"], result["surname"]]


def createEmployee(role: str, supervisor: Employee) -> Employee:
    names = createNames()
    employee = Employee(names, role, supervisor)

    if (supervisor == None):
        print("Created employee: {name}. Supervisor: {supervisorName}".format(
            name=employee.fullName(), supervisorName=None))
    else:
        print("Created employee: {name}. Supervisor: {supervisorName}".format(
            name=employee.fullName(), supervisorName=supervisor.fullName()))

    saveEmployee(employee)

    return employee


def saveEmployee(employee: Employee):

    folder = utilities.findCommonRoot().joinpath("employees")
    folder.mkdir(parents=True, exist_ok=True)

    filename = employee.fullName() + ".json"
    with folder.joinpath(filename).open("w+") as file:
        json.dump(employee.__dict__, file)


def generateOrg(vpCount: int, maxManagerRatio: int, maxWorkerRatio: int):

    vp = createEmployee("VP", None)

    # Generate Mangers
    managers = []
    count = random.randint(1, maxManagerRatio)

    for x in range(count):
        employee = createEmployee("manager", vp)
        managers.append(employee)

    # Generate workers

    for manager in managers:
        count = random.randint(1, maxWorkerRatio)
        for x in range(count):
            employee = createEmployee("worker", manager)



if __name__ == "__main__":
    generateOrg(1, 3, 3)
