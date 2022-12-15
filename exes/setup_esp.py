from cx_Freeze import setup, Executable

executables = [
	Executable("gen_esp32part.py", base="Console", icon="ESP32py.ico"),
	Executable("parttool.py", base="Console", icon="ESP32py.ico")
]

packages = ["idna", "encodings"]
options = {
    'build_exe': {    
        'packages':packages,
	'include_files': ['ESP32py.ico']
    },    
}

setup(
    name = "Esp32 Tool",
    options = options,
    version = "1.0",
    description = 'Espressif ESP32 tool',
    executables = executables
)