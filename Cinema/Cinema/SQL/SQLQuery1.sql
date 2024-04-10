select *
from Магазин
where film_age_control in (
	select film_age_control
	from Магазин
	where film_age_control = 18
	or film_age_control = 6

	)
and film_style in (
	select film_style
	from Магазин
	where film_style = N'Экшен'
	or
		film_style = N'-'
		 or
		film_style = N'-'
	)
and film_country in (
    select film_country
    from Магазин
    where film_country = N'Россия'
    or
    film_country = N'США')

