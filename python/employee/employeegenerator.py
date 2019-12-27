#!/usr/bin/env python3
""" Script to generate employees """

import random
import utilities
import namecreater
import json


class Employee:
    currentEmployeeId = 0

    def __init__(self, names, role, supervisor):
        self.id = Employee.currentEmployeeId
        Employee.currentEmployeeId += 1

        self.names = names
        self.role = role

        if supervisor is not None:
            self.supervisorId = supervisor.id

    def full_name(self) -> str:
        return " ".join(self.names)


roles = ["CIO", "Senior VP", "VP", "Senior Director", "Director", "Associate Director", "Senior Manager", "Manager",
         "Developer"]


def create_employee(role: str, supervisor: Employee) -> Employee:
    names = namecreater.create_name()
    employee = Employee(names, role, supervisor)

    if supervisor is None:
        print("Created employee: {name}. Supervisor: {supervisorName}".format(
            name=employee.full_name(), supervisorName=None))
    else:
        print("Created employee: {name}. Supervisor: {supervisorName}".format(
            name=employee.full_name(), supervisorName=supervisor.full_name()))

    save_employee(employee)

    return employee


def save_employee(employee: Employee):
    folder = utilities.find_common_root().joinpath("employees")
    folder.mkdir(parents=True, exist_ok=True)

    filename = employee.full_name() + ".json"
    with folder.joinpath(filename).open("w+") as file:
        json.dump(employee.__dict__, file)


def generate_level(supervisor, level_counts, index):
    result = []
    count = level_counts[index]
    role = roles[index]

    index = index + 1
    for x in range(count):
        employee = create_employee(role, supervisor)
        result.append(employee)

        if index < len(level_counts):
            generate_level(employee, level_counts, index)

    return result


def generate_org(company_name, *level_counts):
    """Generates a company hierarchy where the company name is the root.

    Args:
        company_name: Name of the company
        level_counts: Array of numbers to generate each level with. Example 1,2 will generate 1 CIO with 2 Senior vps
    """
    global roles

    level_counts = list(level_counts)

    if len(level_counts) > len(roles):
        raise ValueError("Maximum depth of {} reached.".format(len(roles)))

    company_root = Employee([company_name], "Company", None)

    generate_level(company_root, level_counts, 0)


if __name__ == "__main__":
    generate_org("My Company", 2, 3, 4)
