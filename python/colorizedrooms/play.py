from __future__ import print_function
from colors import color

# for i in range(256):
#     print(color('Color #%d' % i, fg=i))

print(color('my string', fg='blue') + color('some text',
                                            fg='red', bg='yellow', style='underline'))
# print(color('some text', fg='red', bg='yellow', style='underline'))

# DREW
