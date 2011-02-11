import BooObjectValidationDsl.Validation
import System.Collections.Generic

rule_for "LoginForm":
  
  validate def(x):
          
      results = List[of ValidationResult]()
      
      if string.IsNullOrEmpty(x.Get("Username")) or string.IsNullOrEmpty(x.Get("Password")):
        results.Add(ValidationResult("Username and Password are required", ResultType.Fail))
        return results
      
      if len(x.Get("Password")) < 6:
        results.Add(ValidationResult("Password must contain at least 6 characters", ResultType.Fail))      	          
           
      return results
      
rule_for "User":
  
  validate def(x):
          
      results = List[of ValidationResult]()
      
      if string.IsNullOrEmpty(x.Username) or string.IsNullOrEmpty(x.Password):
        results.Add(ValidationResult("Username and Password are required", ResultType.Fail))
        return results
        
      if x.Username.Contains("fourletterword") or x.Password.Contains("fourletterword"):
        results.Add(ValidationResult("Username and password cannot contain 4 letter words", ResultType.Fail))
                       
      return results      
  
