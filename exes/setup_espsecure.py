from cx_Freeze import setup, Executable

executables = [Executable("espsecure.py", base="Console", icon="ESP32py.ico")]

packages = ["idna"]
options = {
    'build_exe': {    
        'packages':packages,
	'include_files': ['ESP32py.ico']
    },    
}

setup(
    name = "Esp32 Secure",
    options = options,
    version = "1.0",
    description = 'Espressif secure tool',
    executables = executables
)