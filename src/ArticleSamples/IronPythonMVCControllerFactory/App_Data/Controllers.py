from IronPythonMVCControllerFactory.Controllers import *

def home():
    hc = HomeController()
    hc.ViewData["WelcomeMessage"] = "Welcome to Code Voyeur"
    return hc
    
def profile():        
    pc = ProfileController()
    pc.ViewData["WelcomeMessage"] = "Update your profile."
    return pc
    
def news():
    nc = NewsController()
    nc.ViewData["WelcomeMessage"] = "News about Code Voyeur"
    return nc    