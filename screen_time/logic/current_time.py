from datetime import datetime


def get_current_time():
    return datetime.now().strftime("%d-%m-%Y")
    #return "31-05-2024"