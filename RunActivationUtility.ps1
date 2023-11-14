# Запрашиваем у пользователя ключ продукта
$productKey = Read-Host -Prompt "Введите ключ продукта"

# Запускаем исполняемый файл C# и передаем ключ продукта
Start-Process -FilePath "./ActivationUtility.exe" -ArgumentList $productKey -Wait -NoNewWindow

# Вывод результатов будет автоматически отображаться в консоли
