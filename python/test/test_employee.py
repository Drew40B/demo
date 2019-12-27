import unittest
from unittest.mock import patch, MagicMock

from ..employee import employeegenerator, namecreater


class CreateEmployee(unittest.TestCase):

    @patch("employeegenerator.namecreater.create_name")
    @patch("employeegenerator.save_employee")
    def test_create_employee_happy_path_with_no_supervisor(self, mock_save, mock_name):
        # setup mocks
        employeegenerator.save_employee = mock_save
        namecreater.create_name = mock_name
        mock_name.return_value = ["A", "B"]

        # create an employee
        employee = employeegenerator.create_employee("CIO", None)

        # verify
        self.assertEqual(employee.role, "CIO")
        self.assertEqual(employee.names[0], "A")
        self.assertEqual(employee.names[1], "B")

        mock_save.assert_called()
        mock_name.assert_called()

    @patch("employeegenerator.namecreater.create_name")
    @patch("employeegenerator.save_employee")
    def test_create_employee_happy_path_with_supervisor(self, mock_save, mock_name):
        # setup mocks
        employeegenerator.save_employee = mock_save
        namecreater.create_name = mock_name
        mock_name.return_value = ["A", "B"]

        # create an employee
        supervisor = employeegenerator.Employee(["A", "B"], "root", None)
        employee = employeegenerator.create_employee("CIO", supervisor)

        # verify
        self.assertEqual(employee.supervisorId, supervisor.id)
        self.assertEqual(employee.role, "CIO")
        self.assertEqual(employee.names[0], "A")
        self.assertEqual(employee.names[1], "B")

        mock_save.assert_called()
        mock_name.assert_called()


class GenerateLevel(unittest.TestCase):

    @patch("employeegenerator.create_employee")
    def test_generate_level_1(self, mock_create: MagicMock):
        # setup mocks
        employeegenerator.create_employee = mock_create
        mock_create.return_value = employeegenerator.Employee(["A", "B"], "test", None)

        # create a level
        supervisor = employeegenerator.Employee(["A", "B"], "root", None)
        result = employeegenerator.generate_level(supervisor, [1], 0)

        # assert
        mock_create.assert_called();
        self.assertEqual(1, len(result));

    @patch("employeegenerator.create_employee")
    def test_generate_level_1_1(self, mock_create: MagicMock):
        # setup mocks
        employeegenerator.create_employee = mock_create
        mock_create.return_value = employeegenerator.Employee(["A", "B"], "test", None)

        # create a level
        supervisor = employeegenerator.Employee(["A", "B"], "root", None)
        result = employeegenerator.generate_level(supervisor, [1, 1], 0)

        # assert
        self.assertEqual(2, mock_create.call_count)
        self.assertEqual(1, len(result));

    @patch("employeegenerator.create_employee")
    def test_generate_level_2_2(self, mock_create: MagicMock):
        # setup mocks
        employeegenerator.create_employee = mock_create
        mock_create.return_value = employeegenerator.Employee(["A", "B"], "test", None)

        # create a level
        supervisor = employeegenerator.Employee(["A", "B"], "root", None)
        result = employeegenerator.generate_level(supervisor, [2, 2], 0)

        # assert
        self.assertEqual(6, mock_create.call_count, "call count missmatch")
        self.assertEqual(2, len(result));

    @patch("employeegenerator.create_employee")
    def test_generate_level_2_2_second_index(self, mock_create: MagicMock):
        # setup mocks
        employeegenerator.create_employee = mock_create
        mock_create.return_value = employeegenerator.Employee(["A", "B"], "test", None)

        # create a level
        supervisor = employeegenerator.Employee(["A", "B"], "root", None)
        result = employeegenerator.generate_level(supervisor, [1, 2], 1)

        # assert
        self.assertEqual(2, mock_create.call_count, "call count missmatch")
        self.assertEqual(2, len(result));


if __name__ == '__main__':
    unittest.main()
