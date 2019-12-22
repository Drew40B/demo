import unittest
from unittest.mock import patch, MagicMock

from ..employee import employeegenerator, namecreater


class CreateEmployee(unittest.TestCase):

    @patch("employeegenerator.namecreater.create_name")
    @patch("employeegenerator.save_employee")

    def test_create_employe_happy_path(self, mock_save, mock_name):

        mock_name.return_value = ["A", "B"]

        # create an employee
        employeegenerator.save_employee = mock_save
        namecreater.create_name = mock_name

        employee = employeegenerator.create_employee("CIO", None)

        # verify
        self.assertEqual(employee.role, "CIO")
        self.assertEqual(employee.names[0], "A")
        self.assertEqual(employee.names[1], "B")

        mock_save.assert_called()
        mock_name.assert_called()



if __name__ == '__main__':
    unittest.main()
